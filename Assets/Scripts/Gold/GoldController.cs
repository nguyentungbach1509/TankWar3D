using Others;
using UnityEngine;
using UnityEngine.UI;

namespace Gold
{
    public class GoldController : Singleton<GoldController>
    {
        [SerializeField] int amount;

        public int Amount => amount;
        [SerializeField] Text goldText;
        [SerializeField] GoldCoin goldPrefab;
        GoldCoin[] golds = new GoldCoin[20];

        void Start()
        {
            for (int i = 0; i < golds.Length; i++)
            {
                GoldCoin gold = Instantiate(goldPrefab, transform);
                gold.gameObject.SetActive(false);
                golds[i] = gold;
            }
        }

        public GoldCoin SpawnGold(Vector3 position)
        {
            foreach (GoldCoin gold in golds)
            {
                if (!gold.gameObject.activeInHierarchy)
                {
                    gold.transform.position = new Vector3(position.x, goldPrefab.transform.position.y, position.z);
                    gold.gameObject.SetActive(true);
                    return gold;
                }
            }
            return null;
        }

        public void UpgradeGoldUI(int number)
        {
            amount += number;
            goldText.text = amount.ToString();
        }
    }
}