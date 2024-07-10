using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class comandosBasicos : MonoBehaviour
{
    public float velocidadePersonagem;
    private Rigidbody2D rbPlayer;
    private float movimentoHorizontal;
    private Animator anim;
    private bool verificarDirecao;
    public float jump;
    public Transform posicaoSensor;
    public bool sensor;
    public int vida;
    public TextMeshProUGUI textoVida;


    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movimentoHorizontal = Input.GetAxisRaw("Horizontal");

        rbPlayer.velocity = new Vector2(movimentoHorizontal * velocidadePersonagem, rbPlayer.velocity.y);

        anim.SetInteger("run", (int)movimentoHorizontal);

        if(movimentoHorizontal> 0 && verificarDirecao==true) {
            direcao();
        }else if(movimentoHorizontal<0 && verificarDirecao==false)
        {
            direcao();
        }

        pular();
        detectarChao();

        textoVida.text = vida.ToString();
    }

    public void direcao()
    {
        verificarDirecao = !verificarDirecao;

        float x = transform.localScale.x * -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    public void pular()
    {
        if (Input.GetButtonDown("Jump") && sensor==true)
        {
            rbPlayer.AddForce(new Vector2(0, jump));
        }

        anim.SetBool("sensor", sensor);
    }


    public void detectarChao()
    {
        sensor = Physics2D.OverlapCircle(posicaoSensor.position, 0.25f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coletavel"))
        {
            vida += 1;
            Destroy(collision.gameObject);
        }
    }
}
