using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Linq;

public class TestAnimaciones
{
    private bool sceneLoaded = false;
    private const string DialogKey = "DialogShown";
    GameObject player;
    MovimientoJugador moveScript;
    Animator animator;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        PlayerPrefs.SetInt(DialogKey, 1);//Desactivar el dialogo al iniciar la escena
        
        // Cargar la escena inicial
        if (!sceneLoaded)
        {
            SceneManager.LoadScene("Inicial map");
            sceneLoaded = true;
            yield return null;
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            moveScript = player.GetComponent<MovimientoJugador>();
            animator = player.GetComponent<Animator>();
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator IdleAnimationTest()
    {
        moveScript.SetInput(Vector2.right);
        yield return new WaitForSeconds(0.5f); // Esperar un momento para que la animación se reproduzca

        moveScript.SetInput(Vector2.zero); // Resetear el input
        yield return new WaitForSeconds(0.5f); // Esperar un momento para que la animación se reproduzca

        Assert.IsTrue(animator.GetCurrentAnimatorClipInfo(0).Any(c => c.clip.name.Contains("idle_right_animation")), "La animación de correr hacia abajo no se está reproduciendo correctamente.");
    }
    

    [UnityTest]
    public IEnumerator DownAnimationTest()
    {
        moveScript.SetInput(Vector2.zero);
        yield return new WaitForSeconds(0.5f);
        
        moveScript.SetInput(Vector2.down);
        yield return new WaitForSeconds(1f); // Esperar un momento para que la animación se reproduzca

        Assert.IsTrue(animator.GetCurrentAnimatorClipInfo(1).Any(c => c.clip.name.Contains("run_front_animation")), "La animación de correr hacia abajo no se está reproduciendo correctamente.");
    }

    [UnityTest]
    public IEnumerator UpAnimationTest()
    {
        moveScript.SetInput(Vector2.zero); // Resetear el input antes de probar la animación hacia arriba
        yield return new WaitForSeconds(0.5f);
        
        moveScript.SetInput(Vector2.up);
        yield return new WaitForSeconds(1f); // Esperar un momento para que la animación se reproduzca

        Assert.IsTrue(animator.GetCurrentAnimatorClipInfo(1).Any(c => c.clip.name.Contains("run_back_animation")), "La animación de correr hacia arriba no se está reproduciendo correctamente.");
    }

    [UnityTest]
    public IEnumerator LeftAnimationTest()
    {
        moveScript.SetInput(Vector2.zero); // Resetear el input antes de probar la animación hacia la izquierda
        yield return new WaitForSeconds(0.5f);
        
        moveScript.SetInput(Vector2.left);
        yield return new WaitForSeconds(1f); // Esperar un momento para que la animación se reproduzca

        Assert.IsTrue(animator.GetCurrentAnimatorClipInfo(1).Any(c => c.clip.name.Contains("run_left_animation")), "La animación de correr hacia la izquierda no se está reproduciendo correctamente.");
    }
}
