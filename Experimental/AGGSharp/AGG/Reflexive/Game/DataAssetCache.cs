using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NPack.Interfaces;

namespace Reflexive.Game
{
    public class DataAssetCache<T>
         where T : IEquatable<T>, IComparable<T>, IComputable<T>, IConvertible, IFormattable, ICommonNumericalOperations<T>, ITrigonometricOperations<T>
    {
        DataAssetTree m_DataAssetTree;

        Dictionary<Type, Dictionary<String, GameObject<T>>> m_AssetCache = new Dictionary<Type, Dictionary<string, GameObject<T>>>();

        private static DataAssetCache<T> s_GlobalAssetCache = new DataAssetCache<T>();

        public static DataAssetCache<T> Instance
        {
            get
            {
                return s_GlobalAssetCache;
            }
        }

        private DataAssetCache()
        {
        }
        
        public void SetAssetTree(DataAssetTree dataAssetTree)
        {
            m_DataAssetTree = dataAssetTree;
        }

        public bool AssetExists(Type GameObjectType, String AssetName)
        {
            String PathToAsset = m_DataAssetTree.GetPathToAsset(GameObjectType.Name, AssetName);
            if (PathToAsset == null)
            {
                return false;
            }

            return true;
        }

        public GameObject<T> GetCopyOfAsset(Type GameObjectType, String AssetName)
        {
            // TODO: this code below seems like it should work and would be better (use the copy of the asset in the cache).
            /*
            GameObject asset = GetAsset(GameObjectType, AssetName);
            MemoryStream memoryStream = new MemoryStream();

            XmlTextWriter xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            asset.SaveXML(xmlWriter);

            memoryStream.Position = 0;

            XmlTextReader xmlReader = new XmlTextReader(memoryStream);
            return GameObject.Load(xmlReader);
             */

            String PathToAsset = m_DataAssetTree.GetPathToAsset(GameObjectType.Name, AssetName);
            return LoadGameObjectFromDisk(GameObjectType, AssetName, PathToAsset);
        }

        public GameObject<T> GetAsset(Type GameObjectType, String AssetName)
        {
            if(AssetName == null)
            {
                AssetName = "!Default";
            }

            GameObject<T> Asset = GetAssetFromCache(GameObjectType, AssetName);

            if (Asset == null)
            {
                String PathToAsset = m_DataAssetTree.GetPathToAsset(GameObjectType.Name, AssetName);

                if (PathToAsset == null)
                {
                    if(AssetName == "!Default")
                    {
                        // we are trying to load the default item and we don't have one yet.
                        ConstructorInfo constructInfo = GameObjectType.GetConstructor(Type.EmptyTypes);
                        if(constructInfo == null)
                        {
                            throw new System.Exception("You must have a default constructor defined for '" + GameObjectType.Name + "' for default game object creation to work.");
                        }
                        GameObject<T> defaultGameObjectItem = (GameObject<T>)constructInfo.Invoke(null);
                        String DefaultAssetPath = m_DataAssetTree.Root + Path.DirectorySeparatorChar + "Default";
                        if (!Directory.Exists(DefaultAssetPath))
                        {
                            Directory.CreateDirectory(DefaultAssetPath);
                        }
                        String PathName = DefaultAssetPath + Path.DirectorySeparatorChar + AssetName + "." + GameObjectType.Name;
                        defaultGameObjectItem.SaveXML(PathName);

                        AddAssetToCache(GameObjectType, AssetName, defaultGameObjectItem);
                        return defaultGameObjectItem;
                    }
                    else
                    {
                        throw new System.Exception("'" + GameObjectType.Name + "' named '" + AssetName + "' does not exist.");
                    }
                }

                GameObject<T> gameObjectItem = LoadGameObjectFromDisk(GameObjectType, AssetName, PathToAsset);

                AddAssetToCache(GameObjectType, AssetName, gameObjectItem);

                return gameObjectItem;
            }

            return Asset;
        }

        private static GameObject<T> LoadGameObjectFromDisk(Type GameObjectType, String AssetName, String PathToAsset)
        {
            Type[] ParamsLoadTakes = new Type[] { typeof(String) };
            // TODO: more checking for right function. Must be static must return a GameObject.
            MethodInfo LoadFunction = GameObjectType.GetMethod("Load", ParamsLoadTakes);

            if (LoadFunction == null)
            {
                throw new System.Exception("You must implement the load function on '" + GameObjectType.Name + "'.\n"
                    + "It will look like this, \n 'public new static GameObject Load(String PathName)'.");
            }

            object[] ParamsToCallLoadWith = new object[] { PathToAsset };
            GameObject<T> gameObjectItem;
            try
            {
                gameObjectItem = (GameObject<T>)LoadFunction.Invoke(null, ParamsToCallLoadWith);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }

            if (gameObjectItem == null)
            {
                throw new System.Exception("The load failed for the '" + GameObjectType.Name + "' named '" + AssetName + "'.");
            }
            return gameObjectItem;
        }

        private GameObject<T> GetAssetFromCache(Type GameObjectType, String AssetName)
        {
            Dictionary<string, GameObject<T>> gameObjectClassDictionary;
            if (m_AssetCache.TryGetValue(GameObjectType, out gameObjectClassDictionary))
            {
                GameObject<T> gameObjectItem;
                if (gameObjectClassDictionary.TryGetValue(AssetName, out gameObjectItem))
                {
                    return gameObjectItem;
                }                    
            }

            return null;
        }

        private void AddAssetToCache(Type GameObjectType, String AssetName, GameObject<T> Asset)
        {
            Dictionary<string, GameObject<T>> gameObjectClassDictionary;
            if (!m_AssetCache.TryGetValue(GameObjectType, out gameObjectClassDictionary))
            {
                // create the dictionary
                gameObjectClassDictionary = new Dictionary<string, GameObject<T>>();
                m_AssetCache.Add(GameObjectType, gameObjectClassDictionary);
            }

            GameObject<T> itemInCach;
            if (gameObjectClassDictionary.TryGetValue(AssetName, out itemInCach))
            {
                throw new System.Exception("The '" + GameObjectType.Name + "' asset named '" + AssetName + "' is already in the cache.");
            }

            gameObjectClassDictionary.Add(AssetName, Asset);
        }

        public void ModifyOrCreateAsset(GameObject<T> AssetToSave, string DesiredPathHint,  string AssetName)
        {
            if (AssetExists(AssetToSave.GetType(), AssetName))
            {
                // re-save it
                String PathToAsset = m_DataAssetTree.GetPathToAsset(AssetToSave.GetType().Name, AssetName);
                AssetToSave.SaveXML(PathToAsset);
            }
            else
            {
                // create the file and save the asset
                String DesiredAssetPath = m_DataAssetTree.Root + Path.DirectorySeparatorChar + DesiredPathHint;
                if (!Directory.Exists(DesiredAssetPath))
                {
                    Directory.CreateDirectory(DesiredAssetPath);
                }
                String PathName = DesiredAssetPath + Path.DirectorySeparatorChar + AssetName + "." + AssetToSave.GetType().Name;
                AssetToSave.SaveXML(PathName);

                AddAssetToCache(AssetToSave.GetType(), AssetName, AssetToSave);
                m_DataAssetTree.AddItemToTree(PathName);
            }
        }
    }
}
