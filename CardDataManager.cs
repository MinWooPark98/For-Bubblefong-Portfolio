using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public class CardDataManager : MonoBehaviour
    {
        // --------------------------------------------------------------------
        // Static Instance
        // --------------------------------------------------------------------
        private static CardDataManager s_Instance = null;

        public static CardDataManager Instance
        {
            get
            {
                return s_Instance;
            }
        }

        public static bool IsInstance()
        {
            return (s_Instance == null) ? false : true;
        }

        [SerializeField]
        private Transform parentCardGroup = null;
        [SerializeField]
        private CardData prefabCard = null;

        private bool isMatching = false;          // ��ġ ���(����, ����) ���� ���� ������

        private List<CardData> listCard = new List<CardData>();


        private void Awake()
        {
            s_Instance = this;
        }

        private void OnDestroy()
        {
            s_Instance = null;
        }

        public void MakeCard(int[] _cardArray)
        {
            listCard = new List<CardData>();

            for (int i = 0; i < _cardArray.Length; i++)
            {
                CardData card = Instantiate(prefabCard, parentCardGroup);
                card.SetIndex(listCard.Count);
                card.SetCardNumber(_cardArray[i]);
                listCard.Add(card);
            }
        }

        public void ShowCardAll()
        {
            for (int i = 0; i < listCard.Count; i++)
            {
                listCard[i].ShowCard();
            }
        }

        public void HideCardAll()
        {
            for (int i = 0; i < listCard.Count; i++)
            {
                listCard[i].HideCard();
            }
        }

        public void HideCard(int _idx)
        {
            listCard[_idx].HideCard();
        }

        public void SetCardNumber(int _idx, int _cardNumber)
        {
            listCard[_idx].SetCardNumber(_cardNumber);
        }

        public int GetCardNumber(int _idx)
        {
            return listCard[_idx].GetCardNumber();
        }

        public CardData GetCardData(int _idx)
        {
            return listCard[_idx];
        }

        public void FlipOnCard(int _idx)
        {
            listCard[_idx].FlipOnCard();
        }

        public void FlipOffCard(int _idx)
        {
            listCard[_idx].FlipOffCard();
        }

        public void SetOpen(int _idx)
        {
            listCard[_idx].SetOpen();
        }

        /// <summary>
        /// ��Ī ���� ����������
        /// </summary>
        /// <returns></returns>
        public bool IsMatching()
        {
            return isMatching;
        }

        public void MatchResult(int[] _arrayFlipCard, bool _bResult)
        {
            StartCoroutine(Routine_MatchResult(_arrayFlipCard, _bResult));
        }

        private IEnumerator Routine_MatchResult(int[] _arrayFlipCard, bool _isSuccess)
        {
            isMatching = true;

            bool bBothFlipOn;
            while (true)
            {
                bBothFlipOn = true;
                for (int i = 0; i < _arrayFlipCard.Length; ++i)
                {
                    if (listCard[_arrayFlipCard[i]].GetFlipping() == true)
                    {
                        bBothFlipOn = false;
                        break;
                    }
                }

                if (bBothFlipOn)
                {
                    break;
                }

                yield return null;
            }

            // ī�� ��Ī �����ߴٸ� SetOpen
            if (_isSuccess)
            {
                for (int i = 0; i < _arrayFlipCard.Length; ++i)
                {
                    listCard[_arrayFlipCard[i]].SetOpen();
                }
                SoundManager.Instance.PlaySE(SoundManager.SE.CARDMATCH_MATCH_SUCCESS);
            }

            yield return new WaitForSeconds(1f);

            // ī�� ��Ī ����,���� ������� FlipOffCard ����
            for (int i = 0; i < _arrayFlipCard.Length; ++i)
            {
                listCard[_arrayFlipCard[i]].FlipOffCard();
            }

            isMatching = false;
        }

        /// <summary>
        /// ������ ī�� ����
        /// </summary>
        /// <returns></returns>
        public int GetFlipCount()
        {
            int nCount = 0;

            for (int i = 0; i < listCard.Count; i++)
            {
                if (listCard[i].IsFlip() == true)
                {
                    nCount++;
                }
            }

            return nCount;
        }

        /// <summary>
        /// ������ ī�� �ε��� ����Ʈ
        /// </summary>
        /// <returns></returns>
        public List<int> GetFlipCardList()
        {
            List<int> listCardIndex = new List<int>();
            for (int i = 0; i < listCard.Count; i++)
            {
                if (listCard[i].IsFlip())
                {
                    listCardIndex.Add(listCard[i].GetIndex());
                }
            }

            return listCardIndex;
        }
    }
}
