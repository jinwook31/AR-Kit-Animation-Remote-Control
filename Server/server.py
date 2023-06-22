from flask import Flask, request, json, jsonify
import pandas as pd

app = Flask(__name__)
@app.route("/manageAR", methods=['POST'])

def manageAR():
    params = request.get_json()
    print("받은 Json 데이터 ", params)

    # Dev -> Server
    if params['Type'] == 'checkChangedStatus':
        while checkStatus(params['Chapter'], params['Course']):
            pass

        # Get chapter & course from DB
        db = pd.read_csv("status.csv")
        Chapter = int(db['Chapter'][0])
        Course = int(db['Course'][0])

        response = {
            "Chapter": Chapter,
            "Course":Course
        }


    # Com -> Server
    elif params['Type'] == 'changeStatus':
        Chapter = params['Chapter']
        Course = params['Course']

        # Update DB with new Chapter/Course
        status = {
            "Chapter": Chapter,
            "Course":Course
        }
        
        status = pd.DataFrame(status, index=[0])
        status.to_csv("status.csv")

        response = {
            "result": "Done"
        }

    return jsonify(response)



def checkStatus(devChapter, devCourse):
    # Check DB values and Compare!
    try:
        db = pd.read_csv("status.csv")

        if db['Chapter'][0] != devChapter or db['Course'][0] != devCourse:
            return False
        return True
    except:
        return True



if __name__ == "__main__":
    app.run(debug=True, host='0.0.0.0', port=2023)

    
