import scrapy

class seuSpider(scrapy.Spider):
    name = "seu"
    allowed_domains = ["seu.edu.cn"]
    start_urls = [
        "http://www.seu.edu.cn"       
    ]

    def parse(self, response):
        filename = 'seu.txt'
        with open(filename, 'w') as f:
            f.write(response.xpath('//p'))