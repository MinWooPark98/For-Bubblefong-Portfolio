using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class PhotonManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private void OnEventCardMatch(EventData _photonEvent)
    {
        switch (_photonEvent.Code)
        {
            case PHOTON_CARDMATCH.EventCode.OP_USER_SETTING:
                {
                    CardMatch.CardMatchManager.Instance.SetUserInfo(
                        (int)_photonEvent.Parameters[PHOTON_CARDMATCH.USER_SETTING.ActorNumber],
                        (string)_photonEvent.Parameters[PHOTON_CARDMATCH.USER_SETTING.NickName],
                        (int)_photonEvent.Parameters[PHOTON_CARDMATCH.USER_SETTING.CharacterID]);
                }
                break;

            case PHOTON_CARDMATCH.EventCode.OP_WAIT_USER:
                //CardMatch.CardMatchManager.Instance.ChangeState
                break;

            case PHOTON_CARDMATCH.EventCode.OP_MATCHING_COMPLETE:
                {
                    //CardMatch.CardMatchManager.Instance.ChangeState
                }
                break;

            case PHOTON_CARDMATCH.EventCode.OP_READY_CEHCK:
                //PhotonManager.Instance.RaiseEvent(PHOTON_CARDMATCH.EventCode.OP_READY_CEHCK, data);
                break;

            case PHOTON_CARDMATCH.EventCode.OP_GAME_START:
                {
                    //CardMatch.CardMatchManager.Instance.GameStart
                    //CardMatch.CardMatchManager.Instance.SetStartTurnActor
                    //CardMatch.CardDataManager.Instance.MakeCard
                }
                break;

            case PHOTON_CARDMATCH.EventCode.OP_TURN:
                {

                    //CardMatch.CardMatchManager.Instance.SetStartTurn
                    //CardMatch.CardMatchManager.Instance.SetTimeOver
                    //CardMatch.CardMatchManager.Instance.SetCurrentTurn
                }
                break;

            case PHOTON_CARDMATCH.EventCode.OP_FLIP:
                {
                    //CardMatch.CardMatchManager.Instance.FlipCardData
                    //CardMatch.CardMatchManager.Instance.SetTimeOver
                }
                break;

            case PHOTON_CARDMATCH.EventCode.OP_FLIP_RESULT:
                {
                    //CardMatch.CardMatchManager.Instance.FlipResultData
                    //CardMatch.CardMatchManager.Instance.SetTimeOver
                }
                break;

            case PHOTON_CARDMATCH.EventCode.OP_GAME_OVER:
                {
                    //CardMatch.CardMatchManager.Instance.GameEnd
                }
                break;
        }
    }
}
