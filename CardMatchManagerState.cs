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
            // 대기 화면 동작
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
            // 게임 매칭 완료
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
                    { PHOTON_CARDMATCH.USER_SETTING.ActorNumber, /*내 actor number*/ },
                    { PHOTON_CARDMATCH.USER_SETTING.NickName, /*내 닉네임*/ },
                    { PHOTON_CARDMATCH.USER_SETTING.UserID , /*내 userId*/ },
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
            // 연출

            ChangeState(CARDMATCH_STATE.TOSS);
        }

        //////////////////////////////////////////////////////////////////////
        // TOSS
        //////////////////////////////////////////////////////////////////////
        private void ENTER_TOSS()
        {
            PopupCoinToss popupCoinToss = UIManager.Instance.MakePopup<PopupCoinToss>();
            popupCoinToss.Set(/*내가 시작 유저인지*/);
            popupCoinToss.SetAction(
                () =>
                {
                    ChangeState(CARDMATCH_STATE.STANDBY);
                });
        }

        private void EXIT_TOSS()
        {
            // 게임 시작 BGM
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
                { PHOTON_CARDMATCH.STANDBY.ActorNumber, /*내 actor number*/ },
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
                if (currentActor == /*내 actor number*/)
                {
                    // 내 턴으로 UI 연출
                    ChangeState(CARDMATCH_STATE.TURN);
                }
                else
                {
                    // 적 턴으로 UI 연출
                    ChangeState(CARDMATCH_STATE.PROCESS_READY);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        // TURN
        //////////////////////////////////////////////////////////////////////
        private void ENTER_TURN()
        {
            // 턴 시간 초기화
        }

        private void EXIT_TURN()
        {
            Dictionary<byte, object> data = new Dictionary<byte, object>
            {
                { PHOTON_CARDMATCH.FLIP_PAIR.CurrentTurnActor,/*내 actor number*/ },
                { PHOTON_CARDMATCH.FLIP_PAIR.FlipCardList, CardDataManager.Instance.GetFlipCardList().ToArray() }
            };

            PhotonManager.Instance.RaiseEvent(PHOTON_CARDMATCH.EventCode.OP_FLIP_PAIR, data);
        }

        private void UPDATE_TURN()
        {
            if (currentActor == PhotonManager.Instance.GetMyActorNumber())
            {
                myInfo.UpdateTime();
                // 내 UI 남은 시간 감소
            }
            else
            {
                enemyInfo.UpdateTime();
                // 적 UI 남은 시간 감소
            }

            // 카드를 뒤집었으면 PROCESS_READY로 상태 전환
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
                if (currentActor == /*내 actor number*/)
                {
                    myInfo.UpdateTime();
                    // 내 UI 남은 시간 감소
                }
                else
                {
                    enemyInfo.UpdateTime();
                    // 적 UI 남은 시간 감소
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        // PROCESS
        //////////////////////////////////////////////////////////////////////
        private void ENTER_PROCESS()
        {
            // 점수 갱신
            if (flipActor == /*내 actor number*/)
            {
                // 내 정보 UI 갱신
            }
            else
            {
                // 적 정보 UI 갱신
            }

            // 매칭 결과 화면에 반영
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
            // 게임 결과 팝업 출력
        }

        private void EXIT_GAMERESULT()
        {

        }

        private void UPDATE_GAMERESULT()
        {

        }
    }
}