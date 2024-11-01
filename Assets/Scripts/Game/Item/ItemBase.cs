using System.Collections;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] Vector2 spawnForce = new Vector2(1,7); 
    [SerializeField] float aliveTimeAfterSpawn = 15;
    bool                    allowCollect = true;


    // 새롭게 생성되능 아이템들을 위한 함수 ===========================
    public void Setup(){
        StartCoroutine(nameof(SpawnItemProcess));
    }

    IEnumerator SpawnItemProcess(){
        //!새로 생성된 itemCoin은 충돌체속성이 isTrigger true로 해놓았다. 
        // 그래서 튀어오를 때 플레이어와 충돌하면 onTrigger 작동할 수 있다.
        allowCollect = false; // 튀어오르는 동안은 아이템 취득이 안되게

        var rb = gameObject.AddComponent<Rigidbody2D>(); // 생성된 게임오브젝트가 물리적 이동을 하려면 리지드바디가 필요
        rb.freezeRotation = true; // 데굴데굴 굴러가지 않도록
        rb.velocity = new Vector2( Random.Range(-spawnForce.x, spawnForce.x), spawnForce.y); //좌우로는 -1,1 사이, 위로는 7

        while( rb.velocity.y >0){  // 튀어오르다가 최고점에 달했을 때 y=0
            yield return null;     // 프레임마다 반복하도록
        }

        // 이 아래는 낙하할 때의 상태를 만드는 것이다.
        allowCollect = true; // 떨어질 때는 아이템 취득이 가능하게
        GetComponent<Collider2D>().isTrigger = false; //  트리거속성을 없애면 온전한 물리적충돌체가 되어서
        //!바닥과 충돌할 때, 바닥위에 서 있일 수 있게 된다.
        //! 이때에 플레이어와 충돌하면 onCollision으로 작동할 수 있다.

        yield return new WaitForSeconds(aliveTimeAfterSpawn); // 아이템 생존기간을 채운후 (아직 획득못하면) 사라지게

        Destroy(gameObject);
    }

    //================================================


    // 새롭게 생성된 아이템들의 공중에 떠서 충돌할 때의 처리

    void OnCollisionEnter2D(Collision2D other){ //! [아이템타일,상자에서 생성된 아이템]은 isTrigger false라서 collider적용
        if (allowCollect && other.collider.CompareTag("Player"))
        { //혹은  other.gameObject.CompareTag()

            GameObject Sound = GameObject.Find("Sounds/GetItem_Destroy");
            if (Sound != null)
            {
                AudioSource audioSource = Sound.GetComponent<AudioSource>();
                if (audioSource != null) audioSource.Play();
            }

            UpdateCollision(other.transform);
            Destroy(gameObject);
        }
    }

    // 이미 배치된 아이템들의 충돌처리, 및 새로 생성된 아이템이 바닥에 떨어진 상황에서의 충돌처리

    void OnTriggerEnter2D(Collider2D other){ //! [미리 배치해둔 아이템]의 경우 collider가 isTrigger true
        if ( allowCollect && other.CompareTag("Player")){

            GameObject Sound = GameObject.Find("Sounds/GetItem_Destroy");
            if (Sound != null)
            {
                AudioSource audioSource = Sound.GetComponent<AudioSource>();
                if (audioSource != null) audioSource.Play();
            }

            UpdateCollision(other.transform);
            Destroy(gameObject);
        }
    }

    

    public abstract void UpdateCollision(Transform target);
}
