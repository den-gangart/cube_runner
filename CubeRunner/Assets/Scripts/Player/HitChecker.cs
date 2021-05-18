using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HitChecker : MonoBehaviour
{

    [SerializeField] ParticleSystem particleSystem;

    private AdvManager advertisment;
    private UIController controllerUI;

    private PlayerMovement _player;

    [SerializeField] private MeshRenderer meshRenderer;
    // Start is called before the first frame update

    void Awake()
    {
        particleSystem.gameObject.SetActive(false);
    }
    void Start()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");

        controllerUI = gameController.GetComponent<UIController>();
        advertisment = gameController.GetComponent<AdvManager>();

        _player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        
        if (Physics.SphereCast(ray, 0.5f*transform.localScale.x, out hit, 0.1f * transform.localScale.x))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject != null && hitObject.tag != "Ground" && hitObject.tag != "Player" && _player.GetAlive())
            {
                MusicPlayer.currentGameStatus = GameStatus.End;
                GetComponent<Animator>().SetBool("Dead", true);
                particleSystem.gameObject.SetActive(true);
                particleSystem.Play();
                StartCoroutine(OnDead());
                _player.SetAlive(false);
            }
        }
    }

    private IEnumerator OnDead()
    {
        yield return new WaitForSeconds(1.5f);
        advertisment.ShowAds();
        controllerUI.OnEndGame();
    }

}
