using UnityEngine;

public class RenderStuff : MonoBehaviour
{
    public Mesh mesh;
    public Material mat;

    private float yAngle;
    private float xAngle;

    private Vector3 meshPosition = Vector3.zero;
    private Quaternion meshRotation = Quaternion.identity;

    private bool isWave = false;

    private float spin = 0f;
 
    void Start()
    {
        
    }

    void Update()
    {
        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");
        bool spaceInput = Input.GetKeyDown(KeyCode.Space);
        bool eInput = Input.GetKeyDown(KeyCode.E);

        

        Vector3 movement = new Vector3(xInput, yInput, 0) * Time.deltaTime * 5f;
        meshPosition += movement; 

        if (spaceInput)
        {   
            Debug.Log("Space key was pressed. Starting spin!");
            spin = 90f;
        }

        if (eInput)
        {
            if(!isWave)
            {
                isWave = true;
            }

            else if(isWave)
            {
                isWave = false;
            }
            
        }

        if(isWave)
        {
            Vector3 waveMovement = new Vector3(Mathf.Sin(Time.time) * 3f, Mathf.Cos(Time.time) * 2f, 0);
            meshPosition += waveMovement * Time.deltaTime;
        }


        if (spin > 0f)
        {
            yAngle += spin * Time.deltaTime;
            xAngle -= spin * Time.deltaTime;
            meshRotation = Quaternion.Euler(xAngle, yAngle, 0);

            spin -= 5f * Time.deltaTime; 
        }

        else
        {
            spin = 0f;
        }

        Matrix4x4 m = Matrix4x4.TRS
            (
                meshPosition,
                meshRotation,
                Vector3.one
            );

        Graphics.DrawMesh(mesh, m, mat, 0);
    }
}