using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public partial class CardMatchManager : MonoBehaviour
    {
        //////////////////////////////////////////////////////////////////////
        // INIT
        //////////////////////////////////////////////////////////////////////
        private void ENTER_INIT()
        {
            // ��� ȭ�� ����
        }

        private void EXIT_INIT()
        {

        }

        private void UPDATE_INIT()
        {
            ChangeState(CARDMATCH_STATE.READY);
        }

        //////////////////////////////////////////////////////////////////////
        // READY
        //////////////////////////////////////////////////////////////////////
        private void ENTER_READY()
        {
            // ���� ��Ī �Ϸ�
        }

        private void EXIT_READY()
        {

        }

        private void UPDATE_READY()
        {

        }

        //////////////////////////////////////////////////////////////////////
        // MATCHING_COMPLETE
        //////////////////////////////////////////////////////////////////////
        private void ENTER_MATCHING_COMPLETE()
        {
            Dictionary<byte, object> data = new Dictionary<byte, object>
                {
                    { PHOTON_CARDMATCH.USER_SETTING.ActorNumber, /*�� actor number*/ },
                    { PHOTON_CARDMATCH.USER_SETTING.NickName, /*�� �г���*/ },
                    { PHOTON_CARDMATCH.USER_SETTING.UserID , /*�� userId*/ },
                };
            PhotonManager.Instance.RaiseEvent(PHOTON_CARDMATCH.EventCode.OP_USER_SETTING, data);
        }

        private void EXIT_MATCHING_COMPLETE()
        {

        }

        private void UPDATE_MATCHING_COMPLETE()
        {
            if (gameStart)
            {
                ChangeState(CARDMATCH_STATE.START);
            }
        }

        //////////////////////////////////////////////////////////////////////
        // START
        //////////////////////////////////////////////////////////////////////
        private void ENTER_START()
        {
            StartCoroutine(Routine_StartFadeIn());
        }

        private void EXIT_START()
        {

        }

        private void UPDATE_START()
        {

        }

        private IEnumerator Routine_StartFadeIn()
        {
            // ����

            ChangeState(CARDMATCH_STATE.TOSS);
        }

        //////////////////////////////////////////////////////////////////////
        // TOSS
        //////////////////////////////////////////////////////////////////////
        private void ENTER_TOSS()
        {
            PopupCoinToss popupCoinToss = UIManager.Instance.MakePopup<PopupCoinToss>();
            popupCoinToss.Set(/*���� ���� ��������*/);
            popupCoinToss.SetAction(
                () =>
                {
                    ChangeState(CARDMATCH_STATE.STANDBY);
                });
        }

        private void EXIT_TOSS()
        {
            // ���� ���� BGM
            CardDataManager.Instance.ShowCardAll();
        }

        private void UPDATE_TOSS()
        {

        }


        //////////////////////////////////////////////////////////////////////
        // STANDBY
        //////////////////////////////////////////////////////////////////////
        private void ENTER_STANDBY()
        {
            Dictionary<byte, object> data = new Dictionary<byte, object>
            {
                { PHOTON_CARDMATCH.STANDBY.ActorNumber, /*�� actor number*/ },
            };

            PhotonManager.Instance.RaiseEvent(PHOTON_CARDMATCH.EventCode.OP_STANDBY, data);
        }

        private void EXIT_STANDBY()
        {

        }

        private void UPDATE_STANDBY()
        {
            if(gameEnd)
            {
                ChangeState(CARDMATCH_STATE.GAMERESULT);
            }

            if (IsStartTurn())
            {
                if (currentActor == /*�� actor number*/)
                {
                    // �� ������ UI ����
                    ChangeState(CARDMATCH_STATE.TURN);
                }
                else
                {
                    // �� ������ UI ����
                    ChangeState(CARDMATCH_STATE.PROCESS_READY);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        // TURN
        //////////////////////////////////////////////////////////////////////
        private void ENTER_TURN()
        {
            // �� �ð� �ʱ�ȭ
        }

        private void EXIT_TURN()
        {
            Dictionary<byte, object> data = new Dictionary<byte, object>
            {
                { PHOTON_CARDMATCH.FLIP_PAIR.CurrentTurnActor,/*�� actor number*/ },
                { PHOTON_CARDMATCH.FLIP_PAIR.FlipCardList, CardDataManager.Instance.GetFlipCardList().ToArray() }
            };

            PhotonManager.Instance.RaiseEvent(PHOTON_CARDMATCH.EventCode.OP_FLIP_PAIR, data);
        }

        private void UPDATE_TURN()
        {
            if (currentActor == PhotonManager.Instance.GetMyActorNumber())
            {
                myInfo.UpdateTime();
                // �� UI ���� �ð� ����
            }
            else
            {
                enemyInfo.UpdateTime();
                // �� UI ���� �ð� ����
            }

            // ī�带 ���������� PROCESS_READY�� ���� ��ȯ
        }


        //////////////////////////////////////////////////////////////////////
        // PROCESS_READY
        //////////////////////////////////////////////////////////////////////
        private void ENTER_PROCESS_READY()
        {
        }

        private void EXIT_PROCESS_READY()
        {

        }

        private void UPDATE_PROCESS_READY()
        {
            if (flipResult)
            {
                ChangeState(CARDMATCH_STATE.PROCESS);
            }
            else
            {
                if (currentActor == /*�� actor number*/)
                {
                    myInfo.UpdateTime();
                    // �� UI ���� �ð� ����
                }
                else
                {
                    enemyInfo.UpdateTime();
                    // �� UI ���� �ð� ����
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        // PROCESS
        //////////////////////////////////////////////////////////////////////
        private void ENTER_PROCESS()
        {
            // ���� ����
            if (flipActor == /*�� actor number*/)
            {
                // �� ���� UI ����
            }
            else
            {
                // �� ���� UI ����
            }

            // ��Ī ��� ȭ�鿡 �ݿ�
            CardDataManager.Instance.MatchResult(arrayFlipCard, flipMatchingPair);
        }

        private void EXIT_PROCESS()
        {
            flipResult = false;
            startTurn = false;
        }

        private void UPDATE_PROCESS()
        {
            if (CardDataManager.Instance.IsMatching() == false)
            {
                if (gameEnd == true)
                {
                    ChangeState(CARDMATCH_STATE.GAMERESULT);
                }
                else
                {
                    ChangeState(CARDMATCH_STATE.STANDBY);
                }                
            }
        }

        //////////////////////////////////////////////////////////////////////
        // GAMERESULT
        //////////////////////////////////////////////////////////////////////
        private void ENTER_GAMERESULT()
        {
            // ���� ��� �˾� ���
        }

        private void EXIT_GAMERESULT()
        {

        }

        private void UPDATE_GAMERESULT()
        {

        }
    }
}