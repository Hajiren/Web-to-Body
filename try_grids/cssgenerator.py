from bs4 import BeautifulSoup 
import json
import cssutils

cssPath="generated.css";
jsonPath="test_5.json";

modify_attrs=['left','top','width','height'];

def parseJson():
	res=[];
	file = open(jsonPath, 'rb');
	text = file.read();
	jsondata=json.loads(text);
	elements=jsondata['layouts'][0]['elements'];
	for element in elements:
		tempdata={};
		tempdata['left']=element['x'];
		tempdata['top']=element['y'];
		tempdata['width']=element['width'];
		tempdata['height']=element['height'];
		res.append(tempdata);
	return res;

def modifyCSS():
	modifydata=parseJson();
	file = open(cssPath, 'rb');
	css = file.read();
	sheet = cssutils.parseString(css);
	file.close();

	i=0;
	for rule in sheet:
		element={};
		selector = rule.selectorText;
		for attr in modify_attrs:
			temp=str(int(modifydata[i][attr]))+"px";
			rule.style[attr]=temp;
		i=i+1;
	
	file = open(cssPath, 'wb');
	file.write(sheet.cssText);
	file.close();

modifyCSS();