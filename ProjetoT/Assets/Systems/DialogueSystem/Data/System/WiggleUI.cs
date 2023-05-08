using UnityEngine;

public class WiggleUI : MonoBehaviour
{
    [SerializeField] private float m_WiggleAmount = 10f;
    [SerializeField] private float m_WiggleSpeed = 2f;

    private float m_OriginalY;
    private float m_Time = 0f;

    private void Start()
    {
        m_OriginalY = transform.localPosition.y;
    }

    private void Update()
    {
        m_Time += Time.deltaTime * m_WiggleSpeed;
        float y = m_OriginalY + Mathf.Sin(m_Time) * m_WiggleAmount * GetScreenProportion();

        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    private float GetScreenProportion()
    {
        float screenHeight = Screen.height;
        float screenProportion = screenHeight / 1080f; // 1080 is a reference height
        return screenProportion;
    }
}