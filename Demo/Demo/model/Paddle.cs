namespace Demo.model
{
    public class Paddle
    {
        private int mTubeIndex;
        private int mPaddlePosAct;
        private int mPaddlePosSp;
        private int mPaddleSpeedSp;

        public Paddle()
        {

        }

        public int TubeIndex
        {
            get { return mTubeIndex; }
            set { mTubeIndex = value; }
        }

        public int PaddlePosAct
        {
            get { return mPaddlePosAct; }
            set { mPaddlePosAct = value; }
        }
        public int PaddlePosSp
        {
            get { return mPaddlePosSp; }
            set { mPaddlePosSp = value; }
        }
        public int PaddleSpeedSp
        {
            get { return mPaddleSpeedSp; }
            set { mPaddleSpeedSp = value; }
        }
    }
}
