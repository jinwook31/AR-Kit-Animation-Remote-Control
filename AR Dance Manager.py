from tkinter import *
from tkinter.font import *
import requests
import json


#카운터 클래스 생성
class Counting:
    cnt = 0
    coursecnt = 0

    #카운터 창 생성
    def __init__(self, counter):
        #Window setting
        counter.title("AR Effect Manager")
        counter.geometry("300x280+100+100")
        counter.resizable(False, False)
        self.contents(counter)
        count.insert(0, 0)
        coursecount.insert(0, 0)
        
    #버튼을 누르면 카운트 업 되는 메소드
    def countUp(self):
        self.cnt += 1
        self.coursecnt = 0
        if(self.cnt >= 7):
            self.cnt = 7
        setStatus(self.cnt, self.coursecnt)  
        count.delete(0, 12)
        count.insert(0, self.cnt)
        coursecount.delete(0, 12)
        coursecount.insert(0, self.coursecnt)
        return self.cnt
    
    #버튼을 누르면 카운트 다운 되는 메소드
    def countDown(self):
        self.cnt -= 1
        self.coursecnt = 0
        if(self.cnt <= 0):
            self.cnt = 0
        setStatus(self.cnt, self.coursecnt)
        count.delete(0, 12)
        count.insert(0, self.cnt)
        coursecount.delete(0, 12)
        coursecount.insert(0, self.coursecnt)
        return self.cnt

    #버튼을 누르면 카운트 업 되는 메소드
    def coursecountUp(self):
        self.coursecnt += 1
        setStatus(self.cnt, self.coursecnt) 
        coursecount.delete(0, 12)
        coursecount.insert(0, self.coursecnt)
        return self.coursecnt
    
    #버튼을 누르면 카운트 다운 되는 메소드
    def coursecountDown(self):
        self.coursecnt -= 1
        if(self.coursecnt <= 0):
            self.coursecnt = 0
        setStatus(self.cnt, self.coursecnt) 
        coursecount.delete(0, 12)
        coursecount.insert(0, self.coursecnt)
        return self.coursecnt
        
    #버튼을 누르면 초기화 되는 메소드
    def reset(self):
        self.cnt = 0
        self.coursecnt = 0
        setStatus(self.cnt, self.coursecnt) 
        count.delete(0, 12)
        count.insert(0, self.cnt)
        coursecount.delete(0, 12)
        coursecount.insert(0, self.cnt)
        return self.cnt

    #윈도우 창 설정
    def contents(self, counter):
        #Tkinter window
        text = Label(counter, text = "Chapter", font = Font(size = 0))
        global count
        count = Entry(counter, width = 12, justify = "right", font = Font(size = 30))
        upCounter = Button(counter, text = "Next", font = Font(size = 10), command = self.countUp)
        downCounter = Button(counter, text = "Prev", font = Font(size = 10), command = self.countDown)

        coursetext = Label(counter, text = "Display Guide (0:Yes / 1: None)", font = Font(size = 0))
        global coursecount
        coursecount = Entry(counter, width = 12, justify = "right", font = Font(size = 30))
        courseupCounter = Button(counter, text = "Next", font = Font(size = 10), command = self.coursecountUp)
        coursedownCounter = Button(counter, text = "Prev", font = Font(size = 10), command = self.coursecountDown)
        coursecounterReset = Button(counter, text = "초기화", font = Font(size = 10), command = self.reset)


        #Window initialize
        text.place(x = 20, y = 0)
        count.place(x = 0, y = 30)
        upCounter.place(x = 100, y = 80, width = 100, height = 50)
        downCounter.place(x = 0, y = 80, width = 100, height = 50)

        coursetext.place(x = 20, y = 150)
        coursecount.place(x = 0, y = 180)
        courseupCounter.place(x = 100, y = 230, width = 100, height = 50)
        coursedownCounter.place(x = 0, y = 230, width = 100, height = 50)
        coursecounterReset.place(x = 200, y = 230, width = 100, height = 50)


# https://bongjacy.tistory.com/entry/%EB%B0%B1%EA%B7%B8%EB%9D%BC%EC%9A%B4%EB%93%9C%EC%97%90%EC%84%9C-%ED%8C%8C%EC%9D%B4%EC%8D%AC-%EC%8B%A4%ED%96%89%ED%95%98%EB%8A%94-%EB%B0%A9%EB%B2%95


def setStatus(chapter, course):
    # URL - 127.0.0.1은 localhost로 대체 가능 
    url = "http://143.248.6.81:2023/manageAR"

    # headers
    headers = {
        "Content-Type": "application/json"
    }

    # data
    temp = {
        "Type": "changeStatus",
        "Chapter": chapter,
        "Course": course
    }
    # 딕셔너리를 JSON으로 변환 
    data = json.dumps(temp)

    response = requests.post(url, headers=headers, data=data)

    
def main():
    setStatus(7, 0)   # Init Settings
    
    counter = Tk()
    Count = Counting(counter)
    counter.mainloop()


if __name__ == '__main__':
    main()

