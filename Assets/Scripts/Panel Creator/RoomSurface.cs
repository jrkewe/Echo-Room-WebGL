using TMPro;
using UnityEngine;

public class RoomSurface : MonoBehaviour
{
    public TextMeshProUGUI roomSurface;
    public TextMeshProUGUI recomendedSurface;
    public TextMeshProUGUI actualSurface;

    //Room surface calculation
    private GameObject floorPrefabs;
    private GameObject[] panelPrefabs;

    private float wallThickness = 0.5f;
    private float floorSurface;
    private float sum;
    private float panelSufrace;
    private float recomendedPanelSurface;

    // Start is called before the first frame update
    void Start()
    {
        floorPrefabs = GameObject.FindGameObjectWithTag("Floor");
        if (floorPrefabs != null)
        {
            //Floor surface
            if (floorPrefabs.transform.localScale.x == 1 && floorPrefabs.transform.localScale.z == 1)
            {
                floorSurface = (floorPrefabs.transform.localScale.x) * (floorPrefabs.transform.localScale.z);
            }
            else if (floorPrefabs.transform.localScale.x == 1 && floorPrefabs.transform.localScale.z != 1)
            {
                floorSurface = (floorPrefabs.transform.localScale.x) * (floorPrefabs.transform.localScale.z - wallThickness);
            }
            else if (floorPrefabs.transform.localScale.x != 1 && floorPrefabs.transform.localScale.z == 1)
            {
                floorSurface = (floorPrefabs.transform.localScale.x - wallThickness) * (floorPrefabs.transform.localScale.z);
            }
            else if (floorPrefabs.transform.localScale.x != 1 && floorPrefabs.transform.localScale.z != 1)
            {
                floorSurface = (floorPrefabs.transform.localScale.x - wallThickness) * (floorPrefabs.transform.localScale.z - wallThickness);
            }
        }
        else if (floorPrefabs == null) 
        {
            floorSurface = 0.0f;
        }
        recomendedPanelSurface = floorSurface * 0.3f;

        roomSurface.text = floorSurface.ToString("0.00");
        recomendedSurface.text = recomendedPanelSurface.ToString("0.00");
    }

    private void Update()
    {
        panelPrefabs = GameObject.FindGameObjectsWithTag("Panel");
        if (panelPrefabs!=null)
        {
                float mem = 0;
                for (int i = 0; i < panelPrefabs.Length; i++)
                {
                    panelSufrace = panelPrefabs[i].transform.localScale.x * panelPrefabs[i].transform.localScale.z;
                    mem += panelSufrace;
                }
                sum = mem;
            
        }
        actualSurface.text = sum.ToString("0.00");

    }
}
