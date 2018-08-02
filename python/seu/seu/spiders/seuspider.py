import scrapy
from scrapy import log
import re
class seuSpider(scrapy.Spider):
    i = 0
    name = "seu"
    allowed_domains = ["seu.edu.cn"]
    #start_urls = [       'http://www.seu.edu.cn/zmzjxz/list.htm','http://rsc.seu.edu.cn','http://rsc.seu.edu.cn','http://rsc.seu.edu.cn/zjxzqnxz/list.htm','http://seugs.seu.edu.cn','http://cfd.seu.edu.cn','http://sp.seu.edu.cn','http://zsb.seu.edu.cn','http://yzb.seu.edu.cn','http://seu.91job.gov.cn','http://cis.seu.edu.cn','http://www.lib.seu.edu.cn','http://archives.seu.edu.cn','http://arch.seu.edu.cn','http://me.seu.edu.cn','http://power.seu.edu.cn','http://radio.seu.edu.cn','http://civil.seu.edu.cn','http://electronic.seu.edu.cn','http://math.seu.edu.cn','http://automation.seu.edu.cn','http://cse.seu.edu.cn','http://physics.seu.edu.cn','http://bme.seu.edu.cn','http://smse.seu.edu.cn','http://rwxy.seu.edu.cn','http://em.seu.edu.cn','http://ee.seu.edu.cn','http://sfl.seu.edu.cn','http://tyx.seu.edu.cn','http://chem.seu.edu.cn','http://tc.seu.edu.cn','http://ins.seu.edu.cn','http://arts.seu.edu.cn','http://law.seu.edu.cn','http://med.seu.edu.cn','http://gw.seu.edu.cn','http://wjx.seu.edu.cn','http://cis.seu.edu.cn','http://cose.seu.edu.cn','http://ic.seu.edu.cn','http://marxism.seu.edu.cn','http://ils.seu.edu.cn','http://rcls.seu.edu.cn','http://smjgs.seu.edu.cn','http://zzb.seu.edu.cn/2013/0510/c2813a30233/page.htm','http://zzb.seu.edu.cn/2013/0510/c2813a30235/page.htm','http://www.miitbeian.gov.cn']   
    start_urls = ['http://www.seu.edu.cn/17414/list.htm']
    def pipei(self,list,string):
        for i in list:
            if i == string:
                return True
        return False
    def parse(self, response):
        
        #scrapy.Request(re.findall(""),callback=self.parse)
        filename = "seu_all.txt"
        with open(filename, "w") as f:
            #log.msg(response.xpath("//a/span/text()").extract())
            for k in response.xpath("//a"):
                try:
                    kk = k.xpath("text()").extract()[0]                    
                    url = k.xpath("@href").extract()[0]
                except:
                    kk = k.xpath("text()").extract()
                    url = k.xpath("@href").extract()
               
                #f.write(kk+";"+url+"\r\n")
                #if self.pipei(['概况','学院简介','本院简介','本系概况','学院概况'],kk):                    
                log.msg("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")
                log.msg(kk)
                if len(kk)==0:
                    continue
                if url[0]!="h":
                    url = response.urljoin(url)
                f.write(kk+"\n")#+";"+url+"\n")
                #print(kk)