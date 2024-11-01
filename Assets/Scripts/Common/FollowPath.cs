using System.Collections;
using UnityEngine;

public enum FollowPath_State{ Idle=0, Move}

public class FollowPath : MonoBehaviour
{
    [SerializeField] Transform target; //실제이동하는 대상의  Transform
    [SerializeField] Transform[] wayPoints; //이동가능한 지점들
    [SerializeField] float waitTime;  //wayPoint도착후 대기시간
    [SerializeField] float timeOffset; // 이동시간 = 거리 * timeOffset   즉 이동시간을 설정하기 위한 것이다.(거리당 걸리는 시간?)

    int wayPointCount; // 이동 가능한 wayPoint 갯수
    int currentIndex = 0; // 현재 wayPoint 인덱스

    int direction;
    public int Direction => direction;

    public FollowPath_State State {private set; get;} = FollowPath_State.Idle; //초기화

    void Awake()
    {
        //발판 위치(target.position)을 wayPoints[0]의 위치로 설정
        target.position = wayPoints[currentIndex].position;
        wayPointCount = wayPoints.Length;
        currentIndex++;

        StartCoroutine(nameof(Process));
    }

    IEnumerator Process()
    {
        while (true)
        {
            //wayPoints[currentIndex].position 위치까지 이동
            yield return StartCoroutine(MoveAToB(target.position, wayPoints[currentIndex].position));


            //waitTime 시간 동안 대기
            yield return new WaitForSeconds(waitTime);

            // 다음 이동 지점(wayPoint) 설정
            if (currentIndex < wayPointCount - 1) currentIndex++;
            else currentIndex = 0; // 끝까지 가면 다시 처음위치를 목표로
        }
    }
    IEnumerator MoveAToB(Vector3 start, Vector3 end)
    { // 
        float percent = 0;
        float moveTime = Vector3.Distance(start, end) * timeOffset;

        SetDirection(start.x, end.x);
        State = FollowPath_State.Move;

        while (percent < 1)
        {
            percent += Time.deltaTime / moveTime;
            target.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        State = FollowPath_State.Idle; // while반복문이 끝나면 이동완료. 대기상태로
    }

    void SetDirection(float start, float end){ // x좌표로 계산, 양방향, 음방향 결정
        if( end - start !=0) direction = (int)Mathf.Sign(end -start);
        else                direction = 0;
    }

    public void Stop(){ // 움직일 때는 자체적으로 움직이지만, 멈추는 것은 외부에서 중지호출
        StopAllCoroutines();
    }
}
