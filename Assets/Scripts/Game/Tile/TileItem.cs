using UnityEngine;

public class TileItem : TileBase
{
    [Header("Tile Item")] 
    [SerializeField] ItemType itemType = ItemType.Random;//아이템 속성. 초기에는 랜덤으로 해준다.
    [SerializeField] GameObject[] itemPrefabs; //아이템 타일과 상호작용했을 때 생성되는 아이템 프리팹들
    [SerializeField] int coinCount; //아이템의 속성이 코인일 때 코인 갯수
    [SerializeField] Sprite nonBrokeImage; // 아이템 타일의 모든 아이템이 소진되었을 때 출력되는 이미지
    bool isEmpty = false; // 아이템 타일이 비어 있는지 여부

    public override void UpdateCollision()
    {
        // 아이템이 비어있으면, 상호작용하지 않고 return
        if( isEmpty) return;

        base.UpdateCollision();//부모 메소드부터 호출
        // 아이템 생성
        SpawnItem();

    }

    void SpawnItem(){
        if( itemType == ItemType.Random){ //초기 랜덤.
            itemType = (ItemType)Random.Range(0, itemPrefabs.Length); //! 랜덤에서 구체적인 타입 선정됨
        }

        GameObject item = Instantiate(itemPrefabs[(int)itemType], transform.position, Quaternion.identity);
        item.GetComponent<ItemBase>().Setup();

        // 생성한 TileItem이 coin 타입이면, 아이템타일이 소지하고 있는 coinCount를 1감소(플레이어가 1획득)
        if( itemType == ItemType.Coin){ //!구체적인 타입
            coinCount --;
        }

        // 코인타입이 아니거나 코인 일 때 코인 갯수가 0이면 -> 아이템 타일의 이미지를 부서지지 않는 타일 이미지로 교체하고,
        if( itemType != ItemType.Coin || (itemType == ItemType.Coin && coinCount <=0)){
            GetComponent<SpriteRenderer>().sprite = nonBrokeImage;//아이템 타일의 이미지를 빈 타일 이미지로 변경
            isEmpty = true; // 아이템타일과 충돌해도 UpdateCollision(아이템생성코드) 실행 않도록 한다.
        }
    }
}
