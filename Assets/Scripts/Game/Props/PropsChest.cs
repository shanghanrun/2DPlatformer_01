using System.Collections;
using UnityEngine;

public class PropsChest : MonoBehaviour
{
    [SerializeField] GameObject[] itemPrefabs; // 접촉했을 때 생성되는 아이템프리팹들을 저장하는 배열
    [SerializeField] int        itemCount; // 상자에 생성되는 아이템 갯수
    [SerializeField] Sprite openChestImage;  // 열린 상자 이미지
    SpriteRenderer spriteRenderer;
    bool isChestOpen = false;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if ( !isChestOpen && other.CompareTag("Player")){
            isChestOpen = true;
            spriteRenderer.sprite = openChestImage;

            StartCoroutine(nameof(SpawnAllItems));
        }
    }

    IEnumerator SpawnAllItems(){
        int count =0;
        while(count < itemCount){
            int         index = Random.Range(0, itemPrefabs.Length);
            GameObject item = Instantiate(itemPrefabs[index], transform.position, Quaternion.identity);
            item.GetComponent<ItemBase>().Setup();

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f)); //0.01~0.1초간격으로 생성

            count++;
        }

        float destroyTime = 2; 
        StartCoroutine(FadeEffect.Fade(spriteRenderer, 1,0, destroyTime));
        Destroy(gameObject, destroyTime);
        // 2초동안 Fade효과 나타내다가 사라짐. 이것은 아이템이 사라지는 것이 아니라, Chest사라짐
    }
}
