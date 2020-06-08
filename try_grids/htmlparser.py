from bs4 import BeautifulSoup 
import json
import cssutils

types=['p','img','button']

def getTypeIds(bs):
    res={};
    body=bs.body;
    for type in types:
        res[type]=[];
        temps=body.find_all(type);
        for temp in temps:
            res[type].append(temp['id']);
    return res;

def getAllIds(bs):
    res=[];
    body=bs.body;
    for type in types:
        temps=body.find_all(type);
        for temp in temps:
            res.append(temp['id']);
    return res;

def parseCSS(bs):
    elements = {};
    cssFilePath=bs.link['href'];
    file = open(cssFilePath, 'rb');
    css = file.read();
    sheet = cssutils.parseString(css);

    for rule in sheet:
        element={};
        selector = rule.selectorText;
        for attr in rule.style:
            element[attr.name]=attr.value;
        elements[selector]=element;
    return elements;

def genCSSJson(bs):
    res=[];
    elements=parseCSS(bs);
    ids=getAllIds(bs);
    for id in ids:
        json={};
        element=elements['#'+id];
        json['horizontalPreference']='None';
        json['verticalPreference']='None';
        json['fillColorGreenValue']=255;
        json['fillColorBlueValue']=255;
        json['fillColorRedValue']=255;
        json['width']=int(element['width'].split('px')[0]);
        json['height']=int(element['height'].split('px')[0]);
        json['x']=-1;
        json['y']=-1;
        json['isDrawingFill']=1;
        json['isLocked']="false";
        json['type']="ImageElement";
        res.append(json);
    return res;



def generateBasicJson():
    root={'layouts':[]};
    root['layouts'].append(
      {'borderXPadding':28,
      "elementXPadding":32,
      "id":"originalLayout",
      "borderYPadding" : 32,
      "isOptimised" : False,
      "isEdited" : False,
      "canvasWidth" : 1080,
      "userRating" : 0,
      "canvasHeight" : 1920,
      "elementYPadding" : 32,
      "score" : 0,
      "isSaved" : False});
    return root;


file = open('./test.html', 'rb') 
html = file.read() 
bs = BeautifulSoup(html,"html.parser")

jsontext=generateBasicJson();
elements=genCSSJson(bs);
jsontext['layouts'][0]['elements']=elements;
jsondata = json.dumps(jsontext,indent=4,separators=(',', ': '));


f = open('test.json', 'w')
f.write(jsondata)
f.close()





