using System;
using System.Collections;
using System.Collections.Generic;
using _7DGame_com;
using UnityEngine;

namespace GDGeek
{
    public class BoxColliderCreater : MonoBehaviour, IExecute
    {
        private enum State
        {
            none,
            start,
            running,
            end,
        }

        [SerializeField]
        private int unit_ = 40;
        private Renderer[] renderers_ = null;
        private State state_ = State.none;
        private int index_ = 0;
        private bool hasBounds_ = false;
        private Bounds bounds_;
        private BoxCollider collider_ = null;
        public BoxCollider box => collider_;

        public static GDGeek.DataTask<BoxCollider> GetTask(
            GameObject gameObject,
            int layer = 65535
        ) => GetTask(gameObject, gameObject.transform, layer);

        public static GDGeek.DataTask<BoxCollider> GetTask(
            GameObject gameObject,
            Transform target,
            int layer = 65535
        )
        {
            GDGeek.DataTask<BoxCollider> task = new GDGeek.DataTask<BoxCollider>();
            task.pushFront(() =>
            {
                if (gameObject)
                {
                    BoxColliderCreater bcc = gameObject.AskComponent<BoxColliderCreater>();
                    bcc.target = target;
                    bcc.layer = layer;
                    bcc.onDestroy += delegate
                    {
                        task.resolve(bcc.box);
                    };
                }
            });
            return task;
        }

        public int layer { private get; set; } = 0;

        private Transform target_ = null;
        public Transform target
        {
            get
            {
                if (target_ == null)
                {
                    return this.transform;
                }
                return target_;
            }
            set { target_ = value; }
        }

        public Action onDestroy { get; set; }

        void Awake()
        {
            collider_ = this.gameObject.AskComponent<BoxCollider>();

            collider_.size = Vector3.zero;
            collider_.center = Vector3.zero;
        }

        public bool includeInactive { get; set; } = false;

        void Start()
        {
            renderers_ = target.transform.GetComponentsInChildren<Renderer>(includeInactive);
/*
            foreach (var renderer in renderers_)
            {
                Debug.LogError(renderer.gameObject.longName());
            }*/
            state_ = State.running;
            index_ = 0;
            hasBounds_ = false;
            bounds_ = new Bounds(Vector3.zero, Vector3.zero);
        }

        void Update()
        {
            if (index_ < renderers_.Length)
            {
                Quaternion rotate = this.transform.rotation;
                Vector3 position = this.transform.position;
                Vector3 scale = this.transform.lossyScale;

                this.transform.rotation = Quaternion.identity;
                this.transform.position = Vector3.zero;
                this.transform.setGlobalScale(Vector3.one);

                int count = Mathf.Min(index_ + unit_, renderers_.Length);
                for (; index_ < count; ++index_)
                {
//                    Debug.LogError(index_);

                    int mark = (1 << renderers_[index_].gameObject.layer) & this.layer;
  //                  Debug.LogError(this.layer);
//                    Debug.LogError((1 << renderers_[index_].gameObject.layer));

                    if (
                        renderers_[index_] != null
                        && renderers_[index_].gameObject.activeSelf
                        && mark != 0
                        && renderers_[index_].gameObject.layer != LayerMask.NameToLayer("UI")
                    )
                    {
//                        Debug.LogError(renderers_[index_].bounds.size);
                        if (hasBounds_)
                        {
                            bounds_.Encapsulate(renderers_[index_].bounds);
                        }
                        else
                        {
                            bounds_ = renderers_[index_].bounds;
                            hasBounds_ = true;
                        }
                    }
                }

                this.transform.rotation = rotate;
                this.transform.position = position;
                this.transform.setGlobalScale(scale);
            }
            else
            {
                Destroy(this);
            }
        }

        void OnDestroy()
        {
            collider_.center = bounds_.center;
            collider_.size = bounds_.size;
            //修复Polygen包围盒问题：如果父物体是 Polygen，那么更新包围盒数据为父物体的包围盒（PolygenData.size, PolygenData.center）
            if (transform.parent != null)
            {
                PolygenEntity parentPolygenEntity = transform.parent.GetComponent<PolygenEntity>();
                if (parentPolygenEntity != null)
                {
                    Debug.Log("父物体包含 PolygenEntity 组件");
                    collider_.center = parentPolygenEntity.center;
                    collider_.size = parentPolygenEntity.size;
                }
            }

            onDestroy?.Invoke();
        }

        public void execute()
        {
            Quaternion rotate = this.transform.rotation;
            Vector3 position = this.transform.position;
            Vector3 scale = this.transform.lossyScale;

            this.transform.rotation = Quaternion.identity;
            this.transform.position = Vector3.zero;
            this.transform.setGlobalScale(Vector3.one);

            int count = Mathf.Min(index_ + unit_, renderers_.Length);
            for (int i = 0; i < renderers_.Length; ++i)
            {
                int mark = 1;
                if (this.layer != 0)
                {
                    mark = (1 << renderers_[i].gameObject.layer) & this.layer;
                }

                if (renderers_[i] && renderers_[i].gameObject.activeSelf && mark != 0)
                {
                    if (renderers_[i].gameObject.layer == LayerMask.NameToLayer("UI"))
                    {
                        continue;
                    }

                    if (hasBounds_)
                    {
                        bounds_.Encapsulate(renderers_[i].bounds);
                    }
                    else
                    {
                        bounds_ = renderers_[i].bounds;
                        hasBounds_ = true;
                    }
                }
            }

            this.transform.rotation = rotate;
            this.transform.position = position;
            this.transform.setGlobalScale(scale);

            // DestroyImmediate(this);
        }
    }
}
