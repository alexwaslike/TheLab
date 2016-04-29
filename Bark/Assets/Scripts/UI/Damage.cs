using UnityEngine;

public class Damage : MonoBehaviour {

    private float _life = 1.0f;

    void Update()
    {
        _life -= 1 * Time.deltaTime;
        if( _life <= 0)
        {
            Destroy(gameObject);
        }
    }


}
