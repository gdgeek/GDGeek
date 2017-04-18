using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDGeek
{
    class ProState : UpateState, IPauseabel
    {

        public event Action onInit;
        public event Action onPause;
        public event Action onContinue;

        private bool isFirstEnterThisState = true;
        private bool isPause = false;

        public ProState()
        {
            base.onStart += delegate
            {
                if (isFirstEnterThisState)
                {
                    if (onInit != null)
                    {
                        onInit();
                        isFirstEnterThisState = false;
                    }
                }
            };
            this.addAction("pause", delegate
            {
                if (isPause)
                {
                    return;
                }
                this.pause();
                base.pause();
                isPause = true;
            });
            this.addAction("continue", delegate
            {
                if (!isPause)
                {
                    return;
                }
                this.continuePlay();
                base.continuePlay();
                isPause = false;
            });
        }

        public new void pause()
        {
            if (onPause != null)
            {
                onPause();
            }
        }

        public new void continuePlay()
        {
            if (onContinue != null)
            {
                onContinue();
            }
        }
    }
}
