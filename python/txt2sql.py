import codecs
filename = "seu_tel.txt"
newfilename = filename+".sql"

index = 289

with open(filename) as f:
    with open(newfilename,'w') as f2:
        r = f.readlines()    
        for line in r:
            ll = line.split("\t")
            f2.write("INSERT INTO bop (id, name, relation, name2, intent) VALUES ('"+str(index)+"',N'"+ll[0]+"',N'"+ll[1]+"',N'"+ll[2]+"',N'"+ll[3][:-1]+"')\n")
            index = index+1