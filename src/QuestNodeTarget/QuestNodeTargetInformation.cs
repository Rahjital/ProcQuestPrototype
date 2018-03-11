using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNodeTargetInformation : QuestNodeTargetPossessable
    {
        public QuestInfo info;
        public QuestNodeTarget infoAbout;

        public QuestNodeTargetInformation(QuestInfo info, QuestNodeTarget infoAbout, QuestInfoPosition position)
        {
            this.info = info;
            this.infoAbout = infoAbout;
            this.position = position;
        }

        public override string GetString()
        {
            return String.Format("INFORMATION about {0} of {1}\n--- position: {2}", info.GetStringShort(), infoAbout.GetStringShort(), position.GetString());

        }

        public override string GetStringShort()
        {
            return String.Format("INFORMATION ({0} of {1}) FROM {2}", info.GetStringShort(), infoAbout.GetStringShort(), position.GetStringShort());
        }
    }
}
