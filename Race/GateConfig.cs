namespace Race{
    class GateConfig{
        public string ipAddress;
        public int[] antenas;
        public string mode;
        public string pointType;
        public int filterDelay;
        public int gates;

        public GateConfig(string ipAddress, int[] antenas, string mode, string pointType, int filterDelay){
            this.ipAddress = ipAddress;
            this.antenas = antenas;
            this.mode = mode;
            this.pointType = pointType;
            this.filterDelay = filterDelay;
            this.gates = 0;
            for(int i = 0; i < antenas.Length; i++){
                this.gates |= 1 << antenas[i];
            }
        }
    }
}