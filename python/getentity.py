import json
import requests
def get(name):
    url="http://knowledgeworks.cn:20313/cndbpedia/api/entityAVP?entity="
    #name="东南大学"
    filename = name+".txt"
    result = requests.get(url+name)
    result = json.loads(result.text)
    with open(filename, "w") as f:
        for key in result["av pair"]:    
            if "CATEGORY" not in key[0]:
                key[1] = key[1].replace("\n",";")
                f.write(name+"\t"+key[0]+"\t"+key[1]+"\n")            
                
namelist = ['东南大学图书馆','东南大学档案馆','东南大学建筑学院','东南大学机械工程学院','东南大学能源与环境学院','东南大学信息科学与工程学院','东南大学土木工程学院','东南大学电子科学与工程学院','东南大学数学学院','东南大学自动化学院','东南大学计算机科学与工程学院','东南大学物理学院','东南大学生物科学与医学工程学院','东南大学人文学院','东南大学经济管理学院','东南大学外国语学院','东南大学体育系','东南大学交通学院','东南大学仪器科学与工程学院','东南大学艺术学院','东南大学医学院','东南大学公共卫生学院','东南大学吴健雄学院','东南大学海外教育学院','东南大学软件学院','东南大学马克思主义学院','东南大学学习科学研究中心']
for name in namelist:
    get(name)