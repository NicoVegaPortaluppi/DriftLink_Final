//using System.Collections.Generic;
//using UnityEngine;
///// <summary>
///// player ran out of time or completed all laps, manage the race finish game state and UI
///// </summary>
//namespace SpinMotion
//{
//    public enum RaceFinishType
//    {
//        Lose,
//        Win,
//        Timeout
//    }

//    public class RaceFinish : MonoBehaviour
//    {
//        public GameEvents gameEvents;
//        public RealTimeRacePositionsItem realTimeRacePositions;
//        [Header("Finish Visual Settings (optional)")]
//        public Renderer normalCheckpointRenderer;
//        public List<GameObject> normalCheckpointVisuals = new();
//        public List<GameObject> finishCheckpointVisuals = new();

//        private Collider finishTrigger;
//        private bool raceFinished;

//        private void Awake()
//        {
//            gameEvents.CheckpointPassedEvent.AddListener(OnCheckpointPassed);
//            gameEvents.LapCompletedEvent.AddListener(OnLapCompleted);
//            gameEvents.RestartRaceEvent.AddListener(OnRestartRace);
//            gameEvents.RaceTimeoutEvent.AddListener(OnRaceTimeout);
//        }

//        private void OnCheckpointPassed(int carRacePositionIndex,int checkpointNumber)
//        {
//            if (carRacePositionIndex == 0 && checkpointNumber == RaceData.CheckpointsCount)
//            {
//                if (realTimeRacePositions.Item.LapScores[0] == RaceData.LapsSelected)
//                {
//                    // last checkpoint from last lap
//                    ToggleFinishVisuals(true);
//                }
//            }
//        }

//        private void OnLapCompleted()
//        {
//            if (raceFinished) return; // could be by timeout

//            //if the player completes all the selected laps
//            if (realTimeRacePositions.Item.LapScores[0] > RaceData.LapsSelected)
//            {
//                var raceWon = realTimeRacePositions.Item.GetPlayerRacePosition(0) == 1;
//                if (raceWon)
//                    gameEvents.RaceFinishedEvent.Invoke(RaceFinishType.Win);
//                else
//                    gameEvents.RaceFinishedEvent.Invoke(RaceFinishType.Lose);
//            }
//        }

//        private void OnRestartRace()
//        {
//            ToggleFinishVisuals(false);
//            finishTrigger.enabled = false;
//            raceFinished = false;
//        }

//        private void OnRaceTimeout()
//        {
//            raceFinished = true;
//            gameEvents.RaceFinishedEvent.Invoke(RaceFinishType.Timeout);
//        }

//        private void ToggleFinishVisuals(bool isFinish)
//        {
//            normalCheckpointRenderer.enabled = !isFinish;

//            if (normalCheckpointVisuals.Count > 0)
//            {
//                foreach (var visual in normalCheckpointVisuals)
//                    visual.SetActive(!isFinish);
//            }

//            if (finishCheckpointVisuals.Count > 0)
//            {
//                foreach (var visual in finishCheckpointVisuals)
//                    visual.SetActive(isFinish);
//            }
//        }
//    }
//}

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// player ran out of time or completed all laps, manage the race finish game state and UI
/// </summary>
namespace SpinMotion
{
    public enum RaceFinishType
    {
        Lose,
        Win,
        Timeout
    }

    public class RaceFinish : MonoBehaviour
    {
        public GameEvents gameEvents;
        public RealTimeRacePositionsItem realTimeRacePositions;
        [Header("Finish Visual Settings (optional)")]
        public Renderer normalCheckpointRenderer;
        public List<GameObject> normalCheckpointVisuals = new();
        public List<GameObject> finishCheckpointVisuals = new();

        private Collider finishTrigger;
        private bool raceFinished;

        private void Awake()
        {
            // Initialize finishTrigger - get it from this GameObject
            finishTrigger = GetComponent<Collider>();

            // If not found, try to find it in children
            if (finishTrigger == null)
            {
                finishTrigger = GetComponentInChildren<Collider>();
            }

            // Log warning if still not found
            if (finishTrigger == null)
            {
                Debug.LogWarning("RaceFinish: No Collider found on this GameObject or its children. Make sure there's a trigger collider for race finish.");
            }

            gameEvents.CheckpointPassedEvent.AddListener(OnCheckpointPassed);
            gameEvents.LapCompletedEvent.AddListener(OnLapCompleted);
            gameEvents.RestartRaceEvent.AddListener(OnRestartRace);
            gameEvents.RaceTimeoutEvent.AddListener(OnRaceTimeout);
        }

        private void OnCheckpointPassed(int carRacePositionIndex, int checkpointNumber)
        {
            if (carRacePositionIndex == 0 && checkpointNumber == RaceData.CheckpointsCount)
            {
                if (realTimeRacePositions.Item.LapScores[0] == RaceData.LapsSelected)
                {
                    // last checkpoint from last lap
                    ToggleFinishVisuals(true);
                }
            }
        }

        private void OnLapCompleted()
        {
            if (raceFinished) return; // could be by timeout

            //if the player completes all the selected laps
            if (realTimeRacePositions.Item.LapScores[0] > RaceData.LapsSelected)
            {
                var raceWon = realTimeRacePositions.Item.GetPlayerRacePosition(0) == 1;
                if (raceWon)
                    gameEvents.RaceFinishedEvent.Invoke(RaceFinishType.Win);
                else
                    gameEvents.RaceFinishedEvent.Invoke(RaceFinishType.Lose);
            }
        }

        private void OnRestartRace()
        {
            ToggleFinishVisuals(false);

            // Only disable finishTrigger if it exists
            if (finishTrigger != null)
            {
                finishTrigger.enabled = false;
            }

            raceFinished = false;
        }

        private void OnRaceTimeout()
        {
            raceFinished = true;
            gameEvents.RaceFinishedEvent.Invoke(RaceFinishType.Timeout);
        }

        private void ToggleFinishVisuals(bool isFinish)
        {
            if (normalCheckpointRenderer != null)
            {
                normalCheckpointRenderer.enabled = !isFinish;
            }

            if (normalCheckpointVisuals.Count > 0)
            {
                foreach (var visual in normalCheckpointVisuals)
                {
                    if (visual != null)
                        visual.SetActive(!isFinish);
                }
            }

            if (finishCheckpointVisuals.Count > 0)
            {
                foreach (var visual in finishCheckpointVisuals)
                {
                    if (visual != null)
                        visual.SetActive(isFinish);
                }
            }
        }

        // Add OnDestroy to clean up event listeners
        private void OnDestroy()
        {
            if (gameEvents != null)
            {
                gameEvents.CheckpointPassedEvent.RemoveListener(OnCheckpointPassed);
                gameEvents.LapCompletedEvent.RemoveListener(OnLapCompleted);
                gameEvents.RestartRaceEvent.RemoveListener(OnRestartRace);
                gameEvents.RaceTimeoutEvent.RemoveListener(OnRaceTimeout);
            }
        }
    }
}