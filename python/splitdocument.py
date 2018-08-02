import codecs
import re
filename = "seu_man/cs.txt"
numz = ['0','1','2','3','4','5','6','7','8','9','一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '百', '千', '万', '和','共']
timeee=['年','月','日','时','分']
def pipei(list,string):
    for i in list:
        if i in string:
            return True
    return False
s = []
s2= []
with codecs.open(filename,encoding="utf-8") as f:
    k = f.readlines()
    for line in k:
        result = re.split("，|。|；",line)
        if len(result)>0:
            for rr in result:
                if pipei(numz,rr) and not pipei(timeee,rr) and pipei(['人','个','项','亩','家'],rr):
                    if '个' in rr:
                        name = rr[rr.index("个")+1:]
                        s.append(name)
                        s2.append(rr[:rr.index("个")])
                        
                    if '人' in rr:
                        rr = rr.replace("余","")
                        rr = rr.replace("人","")
                        z = re.findall("[0-9]+",rr)
                        if len(z)>0:
                            ll = z[0]
                            rr = re.sub('[0-9]+','',rr)
                            s.append(rr)
                            s2.append(ll)
                    if '项' in rr:
                        rr = rr.replace("余","")
                        rr = rr.replace("项","")
                        z = re.findall("[0-9]+",rr)
                        if len(z)>0:
                            ll = z[0]
                            rr = re.sub('[0-9]+','',rr)
                            s.append(rr)
                            s2.append(ll)
f1 = open("seu_num.txt",'w')
for i in range(len(s)):
    f1.write("计算机科学与工程学院"+"\t"+s[i]+"\t"+s2[i]+"\t"+"数量\n")