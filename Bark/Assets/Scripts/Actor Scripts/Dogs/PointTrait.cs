using UnityEngine;

// exclamation point always on the N/S/E/W side based on closest other dog
public class PointTrait : DogTrait {

    public GameObject ExclamationSprite;
    public float radius = 2.0f;

    private Vector3 _closestDogLoc;
    private LevelGeneration _levelGen;

    public override void OnAttach(MainCharacter mainCharacter)
    {
        _levelGen = mainCharacter.GameController.LevelGeneration;
        _closestDogLoc = new Vector3(1000.0f, 1000.0f, 1000.0f);
        ExclamationSprite.SetActive(true);
    }

    private float Angle(Vector3 pos1, Vector3 pos2)
    {
        float angle = 0.0f;

        float x = pos1.x - pos2.x;
        float y = pos1.y - pos2.y;

        angle = Mathf.Atan2(y, x);

        return angle;
    }

    void Update()
    {
        if(_levelGen != null) {
            foreach (GameObject obj in _levelGen.Objects) {
                if (obj != null && obj.tag.Equals("Dog") && Mathf.Abs(Vector3.Distance(obj.transform.position, transform.position)) < Mathf.Abs(Vector3.Distance(_closestDogLoc, transform.position)))
                    _closestDogLoc = obj.transform.position;
            }
            
            float angle = Angle(transform.position, _closestDogLoc);
            float x = transform.position.x - (Mathf.Cos(angle) * radius);
            float y = transform.position.y - (Mathf.Sin(angle) * radius);

            ExclamationSprite.transform.position = new Vector3(x, y, 0.0f);
        }
    }

    public override void OnDetach()
    {
        ExclamationSprite.SetActive(false);
    }

}
