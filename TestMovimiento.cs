using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class MovementTestScript
{
    private static bool sceneLoaded = false;
    private GameObject player;
    private MovimientoJugador moveScript;
    private Vector3 startingPosition;
    private const string DialogKey = "DialogShown";

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        PlayerPrefs.SetInt(DialogKey, 1); //Desactivar el dialogo al iniciar la escena

        //Cargar la escena inicial una sola vez y esperar a que cargue
        if (!sceneLoaded)
        {
            SceneManager.LoadScene("Inicial map");
            sceneLoaded = true;
            yield return null;
        }

        //Inicialicar el jugador y su script de movimiento
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            moveScript = player.GetComponent<MovimientoJugador>();
            startingPosition = player.transform.position;
        }

        //Valores iniciales para el script de movimiento y posición del jugador
        moveScript.SetInput(Vector2.zero);
        player.transform.position = startingPosition;

        yield return null;
    }

    [UnityTest]
    public IEnumerator MoveRightTest()
    {
        // Guardar la posición inicial del jugador
        var StartPos = player.transform.position;

        moveScript.SetInput(Vector2.right); // Mover a la derecha

        yield return new WaitForSeconds(0.5f);

        // Guardar la posición final del jugador
        var EndPos = player.transform.position;

        // Verificar que el jugador se haya movido a la derecha y no verticalmente
        Assert.Greater(EndPos.x, StartPos.x, "El jugador no se movió a la derecha como se esperaba.");
        Assert.AreEqual(EndPos.y, StartPos.y, "El jugador se movió verticalmente cuando no debería haberlo hecho.");
    }

    [UnityTest]
    public IEnumerator MoveLeftTest()
    {
        var StartPos = player.transform.position;

        moveScript.SetInput(Vector2.left); // Mover a la izquierda

        yield return new WaitForSeconds(0.5f);

        var EndPos = player.transform.position;

        // Verificar que el jugador se haya movido a la izquierda y no verticalmente
        Assert.Less(EndPos.x, StartPos.x, "El jugador no se movió a la izquierda como se esperaba.");
        Assert.AreEqual(EndPos.y, StartPos.y, "El jugador se movió verticalmente cuando no debería haberlo hecho.");
    }

    [UnityTest]
    public IEnumerator MoveUpTest()
    {
        var StartPos = player.transform.position;

        moveScript.SetInput(Vector2.up); // Mover hacia arriba

        yield return new WaitForSeconds(0.5f);

        var EndPos = player.transform.position;

        // Verificar que el jugador se haya movido hacia arriba y no horizontalmente
        Assert.Greater(EndPos.y, StartPos.y, "El jugador no se movió hacia arriba como se esperaba.");
        Assert.AreEqual(EndPos.x, StartPos.x, "El jugador se movió horizontalmente cuando no debería haberlo hecho.");
    }

    [UnityTest]
    public IEnumerator MoveDownTest()
    {
        var StartPos = player.transform.position;

        moveScript.SetInput(Vector2.down); // Mover hacia abajo

        yield return new WaitForSeconds(0.5f);

        var EndPos = player.transform.position;

        // Verificar que el jugador se haya movido hacia abajo y no horizontalmente
        Assert.Less(EndPos.y, StartPos.y, "El jugador no se movió hacia abajo como se esperaba.");
        Assert.AreEqual(EndPos.x, StartPos.x, "El jugador se movió horizontalmente cuando no debería haberlo hecho.");
    }

    [UnityTest]
    public IEnumerator OppositeSideInputs()
    {
        var StartPos = player.transform.position;

        moveScript.SetInput(new Vector2(1,0) + new Vector2(-1,0)); // Mover diagonalmente

        yield return new WaitForSeconds(0.5f);

        var EndPos = player.transform.position;

        //Verificar que el jugador no se movió
        Assert.AreEqual(EndPos.x, StartPos.x, "El jugador no se quedó en la misma posición como se esperaba.");
    }
    
    [UnityTest]
    public IEnumerator OppositeVerticalInputs()
    {
        var StartPos = player.transform.position;

        moveScript.SetInput(new Vector2(0,1) + new Vector2(0,-1)); // Mover diagonalmente

        yield return new WaitForSeconds(0.5f);

        var EndPos = player.transform.position;

        //Verificar que el jugador no se movió
        Assert.AreEqual(EndPos.y, StartPos.y, "El jugador no se quedó en la misma posición como se esperaba.");
    }

    [UnityTest]
    public IEnumerator UpRightTest()
    {
        var StartPos = player.transform.position;

        moveScript.SetInput(new Vector2(1, 1)); // Mover diagonalmente hacia arriba a la derecha

        yield return new WaitForSeconds(0.5f);

        var EndPos = player.transform.position;

        // Verificar que el jugador se haya movido hacia arriba y a la derecha
        Assert.Greater(EndPos.x, StartPos.x, "El jugador no se movió a la derecha como se esperaba.");
        Assert.Greater(EndPos.y, StartPos.y, "El jugador no se movió hacia arriba como se esperaba.");
    }

    [UnityTest]
    public IEnumerator DownLeftTest()
    {
        var StartPos = player.transform.position;

        moveScript.SetInput(new Vector2(-1, -1)); // Mover diagonalmente hacia abajo a la izquierda

        yield return new WaitForSeconds(0.5f);

        var EndPos = player.transform.position;

        // Verificar que el jugador se haya movido hacia abajo y a la izquierda
        Assert.Less(EndPos.x, StartPos.x, "El jugador no se movió a la izquierda como se esperaba.");
        Assert.Less(EndPos.y, StartPos.y, "El jugador no se movió hacia abajo como se esperaba.");
    }

    [UnityTest]
    public IEnumerator UpLeftTest()
    {
        var StartPos = player.transform.position;

        moveScript.SetInput(new Vector2(-1, 1)); // Mover diagonalmente hacia arriba a la izquierda

        yield return new WaitForSeconds(0.5f);

        var EndPos = player.transform.position;

        // Verificar que el jugador se haya movido hacia arriba y a la izquierda
        Assert.Less(EndPos.x, StartPos.x, "El jugador no se movió a la izquierda como se esperaba.");
        Assert.Greater(EndPos.y, StartPos.y, "El jugador no se movió hacia arriba como se esperaba.");
    }

    [UnityTest]
    public IEnumerator DownRightTest()
    {
        var StartPos = player.transform.position;

        moveScript.SetInput(new Vector2(1, -1)); // Mover diagonalmente hacia abajo a la derecha

        yield return new WaitForSeconds(0.5f);

        var EndPos = player.transform.position;

        // Verificar que el jugador se haya movido hacia abajo y a la derecha
        Assert.Greater(EndPos.x, StartPos.x, "El jugador no se movió a la derecha como se esperaba.");
        Assert.Less(EndPos.y, StartPos.y, "El jugador no se movió hacia abajo como se esperaba.");
    }
}
