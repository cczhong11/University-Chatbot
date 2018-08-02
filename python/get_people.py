import os
folder = "seu_document/"
newfolder="num_seu/"
def pipei(list,string):
    for i in list:
        if i in string:
            return True
    return False

numz = ['0','1','2','3','4','5','6','7','8','9','一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '百', '千', '万', '和','共']
timeee=['年','月','日','时','分']
SS= set()
for filename in os.listdir(folder):
    p = folder + filename
    with open(p) as f:
        r = f.readlines()
        with open("all_num_seu",'a') as f2:
            for line in r:
                result = line.split("\t")
                if pipei(numz,result[2]) and not pipei(['DESC','时间','地址','邮政编码','邮政区码','中文名','始建于','成立于','院长','学者','年','起源'],result[1]):
                    f2.write(line.replace("\n","")+'\t数量\n')
                    SS.add(result[1])

f3 = open("all_num_name.txt","w")
for s in SS:
    f3.write(s+'\n')
f3.close()