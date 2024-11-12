using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Keybindings keybinds;
    private float moveDistance = 1.5f;

    private Stack<ICommand> previousInputs = new Stack<ICommand>();
    private ICommand inputToRedo;
    private InputManager inputManager;

    void Start()
    {
        inputManager = InputManager.Instance;

        inputManager.SetCommand("Up", new MoveUpCommand(this));
        inputManager.SetCommand("Down", new MoveDownCommand(this));
        inputManager.SetCommand("Left", new MoveLeftCommand(this));
        inputManager.SetCommand("Right", new MoveRightCommand(this));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > -15.0f)
        {
            inputManager.HandleInput("Left");
        }

        if (transform.position.x < 15.0f)
        {
            inputManager.HandleInput("Right");
        }

        if (transform.position.y > -20.0f)
        {
            inputManager.HandleInput("Down");
        }

        if (transform.position.y < 20.0f)
        {
            inputManager.HandleInput("Up");
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        GameObject.Destroy(gameObject);
        //EditorApplication.ExitPlaymode();
        Application.Quit();
    }

    public void MoveUp()
    {
        transform.position += new Vector3(0, moveDistance, 0);
    }
    public void MoveDown()
    {
        transform.position -= new Vector3(0, moveDistance, 0);
    }
    public void MoveLeft()
    {
        transform.position -= new Vector3(moveDistance, 0, 0);
    }
    public void MoveRight()
    {
        transform.position += new Vector3(moveDistance, 0, 0);
    }
}
