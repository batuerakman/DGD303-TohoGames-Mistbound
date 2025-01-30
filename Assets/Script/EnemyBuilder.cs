using UnityEngine;
using UnityEngine.Splines;
using Utilities;

namespace Shmup {
    public class EnemyBuilder {
        readonly GameObject enemyPrefab;
        SplineContainer spline;
        GameObject weaponPrefab;
        float speed;
        
        public EnemyBuilder(GameObject prefab) {
            enemyPrefab = prefab;
        }
        
        public EnemyBuilder SetSpline(SplineContainer spline) {
            this.spline = spline;
            return this;
        }
        
        public EnemyBuilder SetWeaponPrefab(GameObject prefab) {
            weaponPrefab = prefab;
            return this;
        }
        
        public EnemyBuilder SetSpeed(float speed) {
            this.speed = speed;
            return this;
        }

        public GameObject Build() {
            if (enemyPrefab == null) return null;
            
            GameObject instance = GameObject.Instantiate(enemyPrefab);
            
            if (spline != null) {
                SplineAnimate splineAnimate = instance.GetOrAdd<SplineAnimate>();
                splineAnimate.Container = spline;
                splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
                splineAnimate.ObjectUpAxis = SplineAnimate.AlignAxis.ZAxis;
                splineAnimate.ObjectForwardAxis = SplineAnimate.AlignAxis.YAxis;
                splineAnimate.MaxSpeed = speed;
           
                instance.transform.position = spline.EvaluatePosition(0f);
                splineAnimate.Restart(true);
            }

            return instance;
        }
    }
}