using System;
using System.Collections.Generic;
using System.Linq;
using Code.UI;
using UnityEngine;

namespace Code.Configs
{
    public interface IViewConfigs
    {
        ViewPrefabs GetConfig(ViewNames type);
    }
    
    [CreateAssetMenu(fileName = @"PrefabViewConfigs", menuName = @"1CGS/Configurations/View prefabs", order = 1)]
    public class PrefabViewConfigs : ScriptableObject, IViewConfigs
    {
        [SerializeField] private List<ViewPrefabs> _prefabs;
        
        public ViewPrefabs GetConfig(ViewNames type) => _prefabs.FirstOrDefault(x => x.ViewName == type);
    }

    [Serializable]
    public class ViewPrefabs
    {
        public ViewNames ViewName;
        public View Prefab;
    }
    
    public enum ViewNames
    {
        MainView = 0,
        BuyConsumeView = 1,
        ConvertView = 2
    }
}