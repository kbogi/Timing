namespace Race{
    class GateConfig{
        public string ipAddress;
        public int[] antenas;

        public GateConfig(string ipAddress, int[] antenas){
            this.ipAddress = ipAddress;
            this.antenas = antenas;
        }
    }
}