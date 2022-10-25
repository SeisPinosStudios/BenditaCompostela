using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    public List<object> objectList;
    void Start()
    {
        //AsyncOperationHandle <List<GameObject>> objAssetList;
        //var objAssets = 
        /*Addressables.LoadAssetsAsync<GameObject>("ObjectAsset", obj =>
          {
              Debug.Log(obj.name + "AA");
          });*/
        int aux;
        for (int i = 0; i < transform.childCount; i++)
        {
            aux = Random.Range(1, 14);
            transform.GetChild(i).gameObject.GetComponent<CardDisplay>().cardData = (CardData)objectList[aux];
        }

    }

    /*IEnumerator LoadAllCardAssets() {

        AsyncOperationHandle<IList<AssetReference>> loadWithIResourceLocations =
        Addressables.LoadAssetsAsync<AssetReference>("ObjectAsset", obj =>
        {
            Debug.Log(obj.SubObjectName);
        });
        yield return loadWithIResourceLocations;
        IList<AssetReference> loadWithLocationsResult = loadWithIResourceLocations.Result;
    }*/


}
