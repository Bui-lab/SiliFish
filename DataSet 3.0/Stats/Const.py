# pylint: disable=C0301, C0303, C0114, C0103

FULL = 0 # represents full scale
SLOW = 1 # represents slow scale or first part of SIF
INTER_I = 2  # represents second part of SIF
INTER_II = 3 # represents third part of SIF
INTER_III = 4 # represents forth part of SIF
FAST = 5  # represents fast scale or last part of SIF
INTER_SCALE = 6 # represents intermediate scale


SWIM_MODE = {0: "Full", 1: "Slow", 2: "Inter-I", 3: "Inter-II", 4: "Inter-III", 5: "Fast", 6: "Intermediate"}
SWIM_MODE_COLOR = {1: "#FF8080", 2: "#D0D080", 3: "#80FF80", 4: "#80D0D0", 5: "#8080FF", 6:"#80FF80"}
SWIM_MODE_SUMMARY_COLOR = {'Slow': "#FF8080", 'Intermediate': "#80FF80", 'Fast': "#8080FF"}

NETWORK_POOLS = {
   #slow
      1:["dI6", "Muscle Slow", "sMN-s-type", "V0v-D", "V1-Hybrid", "V1-Slow", "V2a-VB", "V2b-Mixed"], 
   #Inter-I
      2: ["V0d", "dI6", 
         "Muscle Fast", "Muscle Slow", 
         "sMN-m-type","sMN-ms-type","sMN-ms-type", 
         "V0v-A","V0v-B","V0v-D", 
         "V1-Hybrid", "V1-Slow", 
         "V2a-Type-I-DL",
         "V2b-Mixed"],
   #Inter-II
      3: ["V0d", "dI6", 
            "Muscle Fast", "Muscle Slow", 
            "sMN-m-type","sMN-ms-type","sMN-s-type", 
            "V0v-A","V0v-B","V0v-D", 
            "V1-Hybrid", "V1-Slow", 
            "V2a-Type-I-DL",
            "V2b-Mixed"],
   #Inter-III
      4: ["V0d", "dI6", 
         "Muscle Fast", "Muscle Slow", 
         "pMN", "sMN-m-type","sMN-ms-type","sMN-s-type", 
         "V0v-A","V0v-B",
         "V1-Fast", "V1-Hybrid", "V1-Slow", 
         "V2a-Type-I-DM", "V2a-Type-I-DL", "V2a-Type-II-D", 
         "V2b-Mixed"],
   #Fast
      5:["V0d", 
         "Muscle Fast", "Muscle Slow", 
         "pMN", "sMN-m-type","sMN-ms-type","sMN-s-type", 
         "V0v-A","V0v-B",
         "V1-Fast", "V1-Hybrid",
         "V2a-Type-I-DH", "V2a-Type-I-DM", "V2a-Type-II-D", 
         "V2b-Gly", "V2b-Mixed"],
   #Full Inter 
      6: ["V0d", "dI6", 
         "Muscle Fast", "Muscle Slow", 
         "pMN", "sMN-m-type","sMN-ms-type","sMN-s-type", 
         "V0v-A","V0v-B","V0v-D", 
         "V1-Fast", "V1-Hybrid", "V1-Slow", 
         "V2a-Type-I-DM", "V2a-Type-I-DL", "V2a-Type-II-D", 
         "V2b-Mixed"],
   #inter_increasing
      7: ["V0d",
          "Muscle Fast",
          "pMN","sMN-m-type", "sMN-ms-type", "sMN-s-type",
          "V0v-A" , "V0v-B", 
          "V1-Fast", "V1-Hybrid", 
          "V2a-Type-I-DM", "V2a-Type-II-D",
          "V2b-Mixed"],
   #inter_decreasing
      8: ["dI6","Muscle Slow", "V0v-D", "V1-Slow", "V2a-Type-I-DL"]
}

CELL_POOL_COLOR = {
            "dI6": "#FF3399",
            "Muscle Fast": "#C0C0C0",
            "Muscle Slow": "#FF1C1C",
            "pMN": "#CCE5FF",
            "sMN-ms-type": "#73B6F2",
            "sMN-m-type": "#A0CFFA",
            "sMN-s-type": "#3D8ED9",
            "V0d": "#FFC2E0",
            "V0v-A": "#FFB07C",
            "V0v-B": "#FF9B4F",
            "V0v-D": "#FF8000",
            "V1-Fast": "#FFCCCC",
            "V1-Hybrid": "#FF9999",
            "V1-Slow": "#FF6666",
            "V2a-Type-I-DH": "#A3E699",
            "V2a-Type-I-DL": "#40A040",
            "V2a-Type-I-DM": "#6CCB63",
            "V2a-Type-II-D": "#D0F0C0",
            "V2a-VB": "#267326",
            "V2b-Gly": "#C08CFF",
            "V2b-Mixed": "#7346D6",
        }

CELL_GROUPS = {
            "dI6": "dIV0",
            "Muscle Fast": "Muscle",
            "Muscle Slow": "Muscle",
            "pMN": "MN",
            "sMN-ms-type": "MN",
            "sMN-m-type": "MN",
            "sMN-s-type": "MN",
            "V0d": "dIV0",
            "V0v-A": "V0v",
            "V0v-B": "V0v",
            "V0v-D": "V0v",
            "V1-Fast": "V1",
            "V1-Hybrid": "V1",
            "V1-Slow": "V1",
            "V2a-Type-I-DH": "V2a",
            "V2a-Type-I-DL": "V2a",
            "V2a-Type-I-DM": "V2a",
            "V2a-Type-II-D": "V2a",
            "V2a-VB": "V2a",
            "V2b-Gly": "V2b",
            "V2b-Mixed": "V2b",
        }

CELL_GROUPS2 = {
            "dI6": "dIV0",
            "Muscle Fast": "Muscle",
            "Muscle Slow": "Muscle",
            "pMN": "pMN",
            "sMN-ms-type": "sMN",
            "sMN-m-type": "sMN",
            "sMN-s-type": "sMN",
            "V0d": "dIV0",
            "V0v-A": "V0v",
            "V0v-B": "V0v",
            "V0v-D": "V0v",
            "V1-Fast": "V1",
            "V1-Hybrid": "V1",
            "V1-Slow": "V1",
            "V2a-Type-I-DH": "V2a",
            "V2a-Type-I-DL": "V2a",
            "V2a-Type-I-DM": "V2a",
            "V2a-Type-II-D": "V2a",
            "V2a-VB": "V2a",
            "V2b-Gly": "V2b",
            "V2b-Mixed": "V2b",
        }

CELL_GROUP_COLOR = {
            "dIV0": "#FF3399",
            "Muscle": "#FF1C1C",
            "MN": "#3D8ED9",
            "pMN": "#CCE5FF",
            "sMN": "#73B6F2",
            "V0v": "#FF8000",
            "V1": "#FF6666",
            "V2a": "#267326",
            "V2b": "#7346D6",
        }

CELL_GROUP_POOLS = {
            "dIV0": ["dI6", "V0d"],
            "Muscle": ["Muscle Slow", "Muscle Fast"],
            "MN": ["sMN-s-type","sMN-ms-type", "sMN-m-type", "pMN"],
            "V0v": ["V0v-A","V0v-B","V0v-D"],
            "V1": ["V1-Slow", "V1-Hybrid", "V1-Fast"],
            "V2a": ["V2a-VB","V2a-Type-I-DL","V2a-Type-I-DM","V2a-Type-I-DH","V2a-Type-II-D"],
            "V2b": ["V2b-Mixed", "V2b-Gly"]
        }

TIME_MIN_SIF = [100, 100, 900, 1700, 2500, 3300]
TIME_MAX_SIF = [3850, 650, 1450, 2250, 3050, 3850]

IPSILATERAL_INS = ["V1-Fast", "V1-Hybrid", "V1-Slow", "V1", "V2a-Type-I-DH", "V2a-Type-I-DL", "V2a-Type-I-DM", "V2a-Type-II-D", "V2a-VB", "V2a", "V2b-Gly", "V2b-Mixed", "V2b"]
CONTRALATERAL_INS = ["dI6", "V0d", "dIV0", "V0v-A", "V0v-B", "V0v-D", "V0v"]
