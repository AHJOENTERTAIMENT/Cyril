namespace AHJO {

    [System.Serializable]
    public struct Range<T> {
        public T min, max;

        public Range (T min, T max) {
            this.min = min;
            this.max = max;
        }
    }
}