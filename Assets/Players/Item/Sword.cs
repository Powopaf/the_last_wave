using UnityEngine;

public class Sword : MonoBehaviour
{
    public int Damage { get; }
    public int Protection { get; }
    public (int, int) Potion { get; }
    public void Reset()
    {
    }
    public void Upgrade()
    {
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Zombie"))
        {
            Debug.Log("OOOOOUCH");
        }
    }
}
