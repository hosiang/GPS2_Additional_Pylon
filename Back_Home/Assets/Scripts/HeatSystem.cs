using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSystem : MonoBehaviour {

    [SerializeField] private float heatIncreaseRate; //amount of heat points to increase per time duration
    [SerializeField] private float maxHeatAmount; //max amount of heat ship can handle

    [SerializeField] private float iceAtHand; //amount of ice within player's inventory
    [SerializeField] private float iceCooldownRate; //amount of heat to reduce from usage of ice

    [SerializeField] private float timerDuration; //max time till heat is reduced

    [SerializeField] private int totalShipParts; //total num of ship parts

    [SerializeField] private bool debugMode;

    private Renderer renderer; //for visual debug

    private bool[] isShipPartDamaged = new bool[0];

    private float heatAmount; //current heat buildup
    private float timerCountdown;
    private float colourIndicatorTimer;

    private void Awake() {

        heatAmount = 0f;
        timerCountdown = timerDuration;

        isShipPartDamaged = new bool[totalShipParts];

    }

    // Start is called before the first frame update
    void Start() {

        renderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update() {

        //Debug
        if(renderer.material.color != Color.white && colourIndicatorTimer >= 1) {
            renderer.material.SetColor("_Color", Color.white);
            colourIndicatorTimer = 0f;
        }
        else if (renderer.material.color != Color.white) {
            colourIndicatorTimer += Time.deltaTime;
        }

        timerCountdown -= Time.deltaTime;

        if(timerCountdown <= 0f) {

            heatAmount += heatIncreaseRate + (heatIncreaseRate * numOfDamagedShipParts());

            timerCountdown = timerDuration;

            Debug.Log("Heat Increase");
            renderer.material.SetColor("_Color", Color.yellow);

        }

        if(heatAmount >= maxHeatAmount) {
            Debug.Log("MAX HEAT!!!");
            renderer.material.SetColor("_Color", Color.red);
        }

        if(debugMode) { debug(); }

    }

    private int numOfDamagedShipParts() {

        int damgedParts = 0;

        for(int i = 0; i < totalShipParts; i++) {

            if(isShipPartDamaged[i]) { damgedParts++; }

        }

        Debug.Log(damgedParts + " damaed ship parts");

        return damgedParts;

    }

    public float getHeatAmount()
    {
        return heatAmount;
    }
    public float getMaxHeatAmount()
    {
        return maxHeatAmount;
    }


    void debug() {

        if(Input.GetKeyDown(KeyCode.Q) && iceAtHand > 0) { //Reduce heat by decreasing ice
            iceAtHand--;
            heatAmount -= heatAmount > 0 ? iceCooldownRate : 0;
            Debug.Log("DEBUG MODE: heat reduced");
            renderer.material.SetColor("_Color", Color.green);
        }

        if(Input.GetKeyDown(KeyCode.W)) { //Increase heat and reset countdown timer
            heatAmount++;
            timerCountdown = timerDuration;
            Debug.Log("DEBUG MODE: heat increased");
            renderer.material.SetColor("_Color", Color.yellow);
        }

        if(Input.GetKeyDown(KeyCode.E)) { //Increase ice in inventory
            iceAtHand++;
            Debug.Log("DEBUG MODE: ice increased");
            renderer.material.SetColor("_Color", Color.cyan);
        }

        if(Input.GetKeyDown(KeyCode.R)) { //Reset heat amount
            heatAmount = 0f;
            Debug.Log("DEBUG MODE: heat reset");
            renderer.material.SetColor("_Color", Color.gray);
        }

        //Damage or fix ship parts
        for (int i = 0; i < totalShipParts; i++) {

            if (Input.GetKeyDown((KeyCode)i + (int)KeyCode.Alpha1)) {
                isShipPartDamaged[i] = !isShipPartDamaged[i];
                Debug.Log("DEBUG MODE: ship part " + (i+1) + " damaged: " + isShipPartDamaged[i]);
            }

        }

    }

}
