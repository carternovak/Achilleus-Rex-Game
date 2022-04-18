using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] AudioClip clip;
    [SerializeField] AudioClip roarClip;
    [SerializeField] AudioClip yelpClip;
    [SerializeField] AudioClip growlClip;
    [SerializeField] AudioClip sniffClip;
   // [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    public void Bark()
    {
      //  Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
        audioSource.PlayOneShot(clip);
    }

    public void Roar() {
        audioSource.PlayOneShot(roarClip);
    }

   // public void Growl()
  //  {
    //    audioSource.PlayOneShot(growlClip);
   // }

  //  public void JumpGrowl()
   // {
   //     audioSource.PlayOneShot(growlClip);
  //  }

    public void Yelp()
    {
        audioSource.PlayOneShot(yelpClip);
    }

    public void Sniff()
    {
        audioSource.PlayOneShot(sniffClip);
    }
}
