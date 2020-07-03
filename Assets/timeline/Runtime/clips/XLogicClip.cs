using UnityEngine.Timeline.Data;

namespace UnityEngine.Timeline
{
    public class XLogicClip : XClip<XLogicTrack>
    {
        private GameObject bindObj;

        private LogicClipData Data;

        public XLogicClip(XLogicTrack track, ClipData data) : base(track, data)
        {
        }

        public override string Display
        {
            get
            {
                LogicClipData dta = data as LogicClipData;
                string d = "打击点 ";
                if (dta.logicType?.Length > 0)
                {
                    foreach (var it in dta.logicType)
                    {
                        d += it + ",";
                    }
                    if (d.EndsWith(","))
                    {
                        d = d.Remove(d.Length - 1);
                    }
                }
                return d;
            }
        }


        protected override void OnEnter()
        {
            base.OnEnter();
            var bindTrack = track.root as XBindTrack;
            Data = data as LogicClipData;
            if (Data.showShape && bindTrack?.bindObj)
            {
                bindObj = bindTrack.bindObj;
                if (Data.showShape)
                {
                    track.InitDraw();
                    DrawAttackArea();
                }
            }
        }

        protected override void OnExit()
        {
            base.OnExit();
            if (Data.showShape)
            {
                track?.Clean();
            }
        }


        private void DrawAttackArea()
        {
            var dta = Data;
            if (dta.attackShape == AttackShape.Rect)
            {
                DrawRect(dta.attackArg, dta.attackArg2);
            }
            else if (dta.attackShape == AttackShape.Ring)
            {
                DrawRing(dta.attackArg);
            }
            else if (dta.attackShape == AttackShape.Sector)
            {
                DrawSector(dta.attackArg, dta.attackArg2);
            }
        }


        private void DrawRect(float len, float width)
        {
            var obj = track.draw?.DrawRectangleSolid(bindObj.transform, len, width);
            timeline.BindGo(obj);
        }

        private void DrawRing(float radius)
        {
            var obj = track.draw?.DrawCircleSolid(bindObj.transform, radius);
            timeline.BindGo(obj);
        }

        private void DrawSector(float radius, float angle)
        {
            var obj = track.draw?.DrawSectorSolid(bindObj.transform, angle, radius);
            timeline.BindGo(obj);
        }
    }
}
