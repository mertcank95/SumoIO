using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : EnemyStatusControl
{
    public float moveSpeed = 3f;
    public Transform platform;
    private float minX, maxX, minZ, maxZ;
    private Vector3 targetPoint;
    public bool pushControl { get; set; }
    public float smoothSpeed = 0.02f;//oyuncuyu takip etme h�z�
    private Vector3 smoothVelocity;
    private GameObject player;
    bool soundControl=true;//sesi bir kere �almas� i�in kontrol
    public float Durability { get; set; } = -1;//oyuncunun itmesine kar�� dayan�kl�l�k
    private bool ice;//donmu�ken hareket etmemesi i�in
    private MeshRenderer skinRender;

    private PlayerScore playerScore;
    private GameManager gameManager;

    internal override void Awake()
    {
        base.Awake();
        skinRender = gameObject.GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        platform = GameObject.FindGameObjectWithTag("Platform").GetComponent<Transform>();
        playerScore = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerScore>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        minX = platform.position.x - platform.localScale.x / 2f;
        maxX = platform.position.x + platform.localScale.x / 2f;
        minZ = platform.position.z - platform.localScale.z / 2f;
        maxZ = platform.position.z + platform.localScale.z / 2f;
        
        HedefNoktaBelirle();
        Durability = Random.Range(0, -3);//her d��man farkl� dayan�kl�l��a sahip olams� i�in


    }

    private void FixedUpdate()
    {
        //d��man a�a�� d��erken ba��rma sesi ��karmas� i�in
        if(transform.position.y< 0.7f &&soundControl)
        {
            AudioManager.instance.Play("goat");
            soundControl=false;

        }
    }
    internal override void Update()
    {
        base.Update();
        if(!ice)//donmu�ken bir �ey yapmas�n� istemiyorum
        GetStatusControl();
       

    }
    IEnumerator PushCont()
    {
        //oyuncu yumruk att���nda geriye gitmesini daha net g�stermek i�in
        yield return new WaitForSeconds(0.5f);
        pushControl = false;
    }
    void HedefNoktaBelirle()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        targetPoint = new Vector3(randomX, transform.position.y, randomZ);
    }

    void GetStatusControl()
    {
        if(!ice)
        switch (Status)
        {
            case EnemyStatus.IDLE:
                moveSpeed = 0;
                break;

            case EnemyStatus.WALK:
                if (Vector3.Distance(transform.position, targetPoint) < 0.5f)
                {
                    HedefNoktaBelirle();
                }
                if (!pushControl)
                { 
                    transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
                    StartCoroutine("PushCont");
                }
                break;

            case EnemyStatus.FOLLOW:
                if (!pushControl)
                {
                    Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position,
            player.transform.position, ref smoothVelocity, smoothSpeed);
                transform.position = smoothedPosition;
                    StartCoroutine("PushCont");

                }
                break;


            default:
                break;
        }

    }



    public IEnumerator Ice()
    {

        //oyuncu skill att���nda 2 saniye donmas�n� sa�lad���m metod
        skinRender.materials[0].color = Color.blue;
        ice = true;
        yield return new WaitForSeconds(2.5f);
        ice = false;
        skinRender.materials[0].color = Color.yellow;
      

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyObject"))
        {
            playerScore.AddScore(200);
            gameManager.EnemyCountControl();
            Destroy(gameObject);
        }
    }


}
