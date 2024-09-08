// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using GDGeek;
namespace GDGeek
{
    

    /// <summary>
    /// The Billboard class implements the behaviors needed to keep a GameObject oriented towards the user.
    /// </summary>
    public class Billboard : MonoBehaviour
    {

        void Awake()
        {
            refresh();
        }

        public enum PivotAxis
        {
          // Most common options, preserving current functionality with the same enum order.
          XY,
          Y,
          // Rotate about an individual axis.
          X,
          Z,
          // Rotate about a pair of axes.
          XZ,
          YZ,
          // Rotate about all axes.
          Free
        }

        [SerializeField]
        private bool _enableGlobalScale = false;

        private Vector3 globalScale_;

        /// <summary>
        /// The axis about which the object will rotate.
        /// </summary>
        [Tooltip("Specifies the axis about which the object will rotate.")]
        [SerializeField]
        private PivotAxis _pivotAxis = PivotAxis.Y;
        public PivotAxis pivotAxis
        {
            get { return _pivotAxis; }
            set { _pivotAxis = value; }
        }

        /// <summary>
        /// The target we will orient to. If no target is specified, the main camera will be used.
        /// </summary>
        /// 
        [Tooltip("Specifies the target we will orient to. If no target is specified, the main camera will be used.")]
        [SerializeField]
        private Transform _targetTransform;
        public Transform targetTransform
        {
            get { return _targetTransform; }
            set { _targetTransform = value; }
        }

       
        private void OnEnable()
        {
            globalScale_ = this.transform.lossyScale;
            Update();
        }

        public void refresh()
        {
            
            if (!targetTransform)
            {
                if (Camera.main != null)
                {
                    targetTransform = Camera.main.transform;
                }
                else {
                     return;
                }
               
            }

            // Get a Vector that points from the target to the main camera.
            Vector3 directionToTarget = targetTransform.position - transform.position;
            Vector3 targetUpVector = targetTransform.up;

            // Adjust for the pivot axis.
            switch (pivotAxis)
            {
                case PivotAxis.X:
                    directionToTarget.x = 0.0f;
                    targetUpVector = Vector3.up;
                    break;

                case PivotAxis.Y:
                    directionToTarget.y = 0.0f;
                    targetUpVector = Vector3.up;
                    break;

                case PivotAxis.Z:
                    directionToTarget.x = 0.0f;
                    directionToTarget.y = 0.0f;
                    break;

                case PivotAxis.XY:
                    targetUpVector = Vector3.up;
                    break;

                case PivotAxis.XZ:
                    directionToTarget.x = 0.0f;
                    break;

                case PivotAxis.YZ:
                    directionToTarget.y = 0.0f;
                    break;

                case PivotAxis.Free:
                default:
                    // No changes needed.
                    break;
            }

            // If we are right next to the camera the rotation is undefined. 
            if (directionToTarget.sqrMagnitude < 0.001f)
            {
                return;
            }

            // Calculate and apply the rotation required to reorient the object
            transform.rotation = Quaternion.LookRotation(-directionToTarget, targetUpVector);

            if (_enableGlobalScale) { 
                this.transform.setGlobalScale(globalScale_);
            }
            
        }

        /// <summary>
        /// Keeps the object facing the camera.
        /// </summary>
        void Update()
        {
            refresh();
        }
    }
}
