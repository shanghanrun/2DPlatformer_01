using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [Header("Camera Limit")]
    [SerializeField] float  cameraLimitMinX;
    [SerializeField] float cameraLimitMaxX;

    [Header("Player Limit")]
    [SerializeField] float  playerLimitMinX;
    [SerializeField] float  playerLimitMaxX;

    [Header("Map Limit")]
    [SerializeField] float mapLimitMinY; // 추락 검사를 위해

    [Header("Start Position")]
    [SerializeField]  Vector2 playerPosition;
    [SerializeField]  Vector2 cameraPosition;


    // 외부에서 변수 데이터에 접근하기 위한 프로퍼티 Get프러퍼티
    public float CameraLimitMinX => cameraLimitMinX;
    public float CameraLimitMaxX => cameraLimitMaxX;
    public float PlayerLimitMinX => playerLimitMinX;
    public float PlayerLimitMaxX => playerLimitMaxX;
    public float MapLimitMinY => mapLimitMinY;
    public Vector2 PlayerPosition => playerPosition;
    public Vector2 CameraPosition => cameraPosition;

}
