using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float angulom, anguloM, velocidadAngular;
    public float direccionInicial = 1.0f; // 👈 添加这个：让 Inspector 控制方向
    
    Vector3 angulo;
    float direccion, //1.0f positiva, -1.0f negativa
        rotationAcumulada;

    // Start is called before the first frame update
    void Start()
    {
        angulo = new Vector3(0.0f, 0.0f, 0.0f);
        direccion = direccionInicial;
        rotationAcumulada = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        angulo.z = direccion * velocidadAngular*Time.deltaTime;
        rotationAcumulada += angulo.z;
        transform.Rotate(angulo);
        Debug.Log(transform.rotation.z + " " +
        transform.localRotation.z);

        if (rotationAcumulada > anguloM)
            direccion *= -1.0f;

        if (rotationAcumulada < angulom)
            direccion *= -1.0f; 
    }
}
