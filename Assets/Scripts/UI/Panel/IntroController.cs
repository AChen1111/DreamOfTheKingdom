using Events.ScripctsObject;
using UnityEngine;
using UnityEngine.Playables;

namespace UI.Panel
{
    public class IntroController : MonoBehaviour
    {
        public PlayableDirector director;
        public ObjectEventSO loadMeauEvent;

        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
            director.stopped += DirectorOnstopped;
        }

        private void DirectorOnstopped(PlayableDirector obj)
        {
            Debug.Log(obj.name + " is stopped");
            loadMeauEvent.RaiseEvent(null,this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && director.state == PlayState.Playing)
            {
                director.Stop();
            }
        }
        
    }
}