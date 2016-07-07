using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class SliderScript : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler{
    public Text Selected_Item_Number;
    public static int ElementNumber;
    [Header("Drag Without Buttons")]
    public bool Drag_Only;
    [Header("Manipulate Buttons")]
    public Button UpButton;
    public Button DownButton;
    public Button LeftButton;
    public Button RightButton;
    //public Button Upbutton1;
    [Header("Middle Slide Position")]
    public Transform middle_slide_position;
    [Header("Images Orientation")]
    public Orientation Orient = new Orientation();
    [Header("Step Between Images")]
    public float imagestep;
    [Header("Images Speed")]
    public float speed;
    [Header("Scale Parameters")]
    public float scaleFactor;
    public float maxScale=2.5f;
    Vector2 drag_start_position;
    Vector2 drag_end_position;
    Vector2 touch_position_indncator;
    float minScale;
    public GameObject[] images;
    Vector3 firstSlidePosition;
    Vector3 LastSlidePosition;
 
    Vector3 imagepos;
    float[] aimagepositions;
    int step = 1;
    bool left,right,up,down;
    bool enddrag;
    int move_right_step=0;
    int move_left_step=0;
    int move_down_step = 0;
    int move_up_step = 0;
    // Use this for initialization
    public enum Orientation {
        vertical,
        horizontal
    }
  
    void Start () {
       
        
        if (Drag_Only==true && UpButton != null && DownButton != null && LeftButton != null && RightButton != null) {
            UpButton.gameObject.SetActive(false);
            DownButton.gameObject.SetActive(false);
            LeftButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(false);
        }
        #region  Horizontal Orientation
        if (Orient == Orientation.horizontal) {
            imagestep = imagestep * (Screen.width / 1280f);
            if (UpButton != null && DownButton != null) {
                RightButton.onClick.AddListener(MoveRightButton);
                LeftButton.onClick.AddListener(MoveLeftButton);
                UpButton.gameObject.SetActive(false);
                DownButton.gameObject.SetActive(false);
            }
            aimagepositions = new float[images.Length];
            if (images.Length % 2 == 0) {

                firstSlidePosition = new Vector3(middle_slide_position.position.x - imagestep * images.Length / 2,
                                                     middle_slide_position.position.y,
                                                     middle_slide_position.position.z);
                LastSlidePosition = new Vector3(middle_slide_position.position.x + imagestep * (images.Length / 2 - 1),
                                                     middle_slide_position.position.y,
                                                     middle_slide_position.position.z);

            }
            else {

                firstSlidePosition = new Vector3(middle_slide_position.position.x - imagestep * Mathf.RoundToInt(images.Length / 2),
                                                    middle_slide_position.position.y,
                                                    middle_slide_position.position.z);
                LastSlidePosition = new Vector3(middle_slide_position.position.x + imagestep * Mathf.RoundToInt(images.Length / 2),
                                                    middle_slide_position.position.y,
                                                    middle_slide_position.position.z);

            }
            images[0].transform.position = firstSlidePosition;
            aimagepositions[0] = firstSlidePosition.x;
            imagepos = firstSlidePosition;
            minScale = images[0].transform.localScale.x;
            for (byte i = 1; i < images.Length; ++i) {
                imagepos += Vector3.right * imagestep;

                images[i].transform.position = imagepos;
                aimagepositions[i] = images[i].transform.position.x;
            }
        }
        #endregion

        #region Vertical Oriaentation
        if (Orient == Orientation.vertical) {
            if (LeftButton != null && RightButton != null) {
               
                UpButton.onClick.AddListener(MoveUpButton);
                DownButton.onClick.AddListener(MoveDownButton);
                LeftButton.gameObject.SetActive(false);
                RightButton.gameObject.SetActive(false);

            }
            aimagepositions = new float[images.Length];
            if (images.Length % 2 == 0) {//check for devide by 2

                firstSlidePosition = new Vector3(middle_slide_position.position.x,
                                                     middle_slide_position.position.y - imagestep * images.Length / 2,
                                                     middle_slide_position.position.z);
                LastSlidePosition = new Vector3(middle_slide_position.position.x,
                                                     middle_slide_position.position.y + imagestep * (images.Length / 2 - 1),
                                                     middle_slide_position.position.z);

            }
            else {

                firstSlidePosition = new Vector3(middle_slide_position.position.x,
                                                    middle_slide_position.position.y - imagestep * Mathf.RoundToInt(images.Length / 2),
                                                    middle_slide_position.position.z);
                LastSlidePosition = new Vector3(middle_slide_position.position.x,
                                                    middle_slide_position.position.y + imagestep * Mathf.RoundToInt(images.Length / 2 ),
                                                    middle_slide_position.position.z);

            }
            images[0].transform.position = firstSlidePosition;
            aimagepositions[0] = firstSlidePosition.y;
            imagepos = firstSlidePosition;
            minScale = images[0].transform.localScale.y;
            for (byte i = 1; i < images.Length; ++i) {
                imagepos += Vector3.up * imagestep;

                images[i].transform.position = imagepos;
                aimagepositions[i] = images[i].transform.position.y;
            }
        }
        #endregion
    }

    // Update is called once per frame
    void Update () {
        if (Drag_Only == false||enddrag==true) {
            if (right == true) {
                MoveRight();
            }
            if (left == true) {

                MoveLeft();
            }

            if (down == true) {

                MoveDown();
            }

            if (up == true) {

                MoveUp();
            }
        }
        
    }

    public void MoveRightButton() {
        if (left == true) return;
        right = true;

    }
    public void MoveLeftButton() {
        if (right == true) return;
        Debug.Log(move_right_step);
        left = true;
        //right = !right;
    }
    public void MoveUpButton() {
        up = true;
    }

    public void MoveDownButton() {
        down = true;
    }

    void MoveRight() {//RightMovement Method

        for (byte i = 0; i < images.Length; ++i) {
            float gotoxpos = Mathf.MoveTowards(images[i].transform.position.x, aimagepositions[i] + imagestep, speed * 50 * Time.deltaTime);
            images[i].transform.position = new Vector3(gotoxpos, images[i].transform.position.y, images[i].transform.position.z);
          //  images[i].transform.Translate(new Vector3(aimagepositions[i] + imagestep, images[i].transform.position.y, images[i].transform.position.z));
            if (middle_slide_position.position.x - images[i].transform.position.x < imagestep && middle_slide_position.position.x - images[i].transform.position.x > 0) {
                if (images[i].transform.localScale.x > minScale) {
                    SelectedItem(i);
                }

                float scale_middle_image = SmoothScale(images[i].transform.localScale.x, maxScale, scaleFactor * Time.deltaTime);
                images[i].transform.localScale = new Vector3(scale_middle_image, scale_middle_image, 0);

            }
            else {
                float scale_middle_image = Mathf.MoveTowards(images[i].transform.localScale.x, 1, scaleFactor / 50);
                images[i].transform.localScale = new Vector3(scale_middle_image, scale_middle_image, 0);
                // Debug.Log(minScale);
            }

        }

        if (images[0].transform.position.x >= aimagepositions[0] + imagestep-0.01f) {//chek position if position more than this pos + step

            if (move_right_step >= images.Length) {
                move_right_step = 0;//image array step element go to zero
            }
            if (move_left_step > 0) {
                images[move_left_step - 1].transform.position = firstSlidePosition;
                move_left_step--;
            }
            else {
                images[images.Length - 1 - move_right_step].transform.position = firstSlidePosition;// go to firstslideposition
                move_right_step++;
            }

            for (byte i = 0; i < images.Length; ++i) {
                aimagepositions[i] = images[i].transform.position.x;//save pos of all image  array elements 
            }

            right = false;

        }


    }

    void MoveLeft() {//Left Movement Method

        for (byte i = 0; i < images.Length; ++i) {
            float gotoxpos = Mathf.MoveTowards(images[i].transform.position.x, aimagepositions[i] - imagestep, speed * 50 * Time.deltaTime);
            images[i].transform.position = new Vector3(gotoxpos, images[i].transform.position.y, images[i].transform.position.z);

            if (images[i].transform.position.x - middle_slide_position.position.x < imagestep && images[i].transform.position.x - middle_slide_position.position.x > 0) {
                if (images[i].transform.localScale.x > minScale) {
                    SelectedItem(i);
                }
                float scale_middle_image = SmoothScale(images[i].transform.localScale.x, maxScale, scaleFactor * Time.deltaTime);
                images[i].transform.localScale = new Vector3(scale_middle_image, scale_middle_image, 0);

            }
            else {
                float scale_middle_image = Mathf.MoveTowards(images[i].transform.localScale.x, 1, scaleFactor / 50);
                images[i].transform.localScale = new Vector3(scale_middle_image, scale_middle_image, 0);
            }


        }
       // Debug.Log("aaaaaa");
        if (images[0].transform.position.x <= aimagepositions[0] - imagestep + 0.01) {
            if (move_left_step >= images.Length) {
                move_left_step = 0;
            }
            if (move_right_step > 0) {
                images[images.Length - move_right_step].transform.position = LastSlidePosition;
                move_right_step--;
            }
            else {
                images[move_left_step].transform.position = LastSlidePosition;// new Vector3(aimagepositions[0], 
                move_left_step++;
            }

            for (byte i = 0; i < images.Length; i++) {
                aimagepositions[i] = images[i].transform.position.x;
            }

            left = false;

        }


    }

    void MoveDown() {
        for (byte i = 0; i < images.Length; ++i) {
            float gotoxpos = Mathf.MoveTowards(images[i].transform.position.y, aimagepositions[i] - imagestep, speed * 50 * Time.deltaTime);
            images[i].transform.position = new Vector3(images[i].transform.position.x, gotoxpos, images[i].transform.position.z);

            if (images[i].transform.position.y - middle_slide_position.position.y < imagestep && images[i].transform.position.y - middle_slide_position.position.y > 0) {

                float scale_middle_image = SmoothScale(images[i].transform.localScale.y, maxScale, scaleFactor * Time.deltaTime);
                images[i].transform.localScale = new Vector3(scale_middle_image, scale_middle_image, 0);
                if (images[i].transform.localScale.x > minScale) {
                    SelectedItem(i);
                }

            }
            else {
                float scale_middle_image = Mathf.MoveTowards(images[i].transform.localScale.y, 1, scaleFactor / 50);
                images[i].transform.localScale = new Vector3(scale_middle_image, scale_middle_image, 0);
            }


        }
        //  Debug.Log("aaaaaa");
        if (images[0].transform.position.y <= aimagepositions[0] - imagestep + 1) {
            if (move_down_step >= images.Length) {
                move_down_step = 0;
            }
            if (move_up_step > 0) {
                images[images.Length - move_up_step].transform.position = LastSlidePosition;
                move_up_step--;
            }
            else {
                images[move_down_step].transform.position = LastSlidePosition;// new Vector3(aimagepositions[0], 
                move_down_step++;
            }

            for (byte i = 0; i < images.Length; i++) {
                aimagepositions[i] = images[i].transform.position.y;
            }

            down = false;

        }


    }

    void MoveUp() {
        for (byte i = 0; i < images.Length; ++i) {
            float gotoxpos = Mathf.MoveTowards(images[i].transform.position.y, aimagepositions[i] + imagestep, speed * 50 * Time.deltaTime);
            images[i].transform.position = new Vector3(images[i].transform.position.x, gotoxpos, images[i].transform.position.z);

            if (middle_slide_position.position.y - images[i].transform.position.y < imagestep && middle_slide_position.position.y - images[i].transform.position.y > 0) {
                if (images[i].transform.localScale.x > minScale) {
                    SelectedItem(i);
                }
                float scale_middle_image = SmoothScale(images[i].transform.localScale.y, maxScale, scaleFactor * Time.deltaTime);
                images[i].transform.localScale = new Vector3(scale_middle_image, scale_middle_image, 0);

            }
            else {
                float scale_middle_image = Mathf.MoveTowards(images[i].transform.localScale.y, 1, scaleFactor / 50);
                images[i].transform.localScale = new Vector3(scale_middle_image, scale_middle_image, 0);
                // Debug.Log(minScale);
            }

        }

        if (images[0].transform.position.y >= aimagepositions[0] + imagestep - 0.01f) {//chek position if position more than this pos + step

            if (move_up_step >= images.Length) {
                move_up_step = 0;//image array step element go to zero
            }
            if (move_down_step > 0) {
                images[move_down_step - 1].transform.position = firstSlidePosition;
                move_down_step--;
            }
            else {
                images[images.Length - 1 - move_up_step].transform.position = firstSlidePosition;// go to firstslideposition
                move_up_step++;
            }

            for (byte i = 0; i < images.Length; ++i) {
                aimagepositions[i] = images[i].transform.position.y;//save pos of all image  array elements 
            }

            up = false;

        }
    }

    public float SmoothScale(float min,float max,float step) {
        float value=0;
        min += step;
        value = min;
        if (min>=max) {
            value = max;
        }

       // Debug.Log(value);
        return value;
    }

    public void OnDrag(PointerEventData eventData) {
        if (Orient == Orientation.horizontal) {
            if (Input.mousePosition.x - touch_position_indncator.x > 0) {
                MoveRight();
            }
            if (Input.mousePosition.x - touch_position_indncator.x < 0) {
                MoveLeft();
            }
        }

        if (Orient == Orientation.vertical) {
            if (Input.mousePosition.y - touch_position_indncator.y > 0) {
                MoveUp();
            }
            if (Input.mousePosition.y - touch_position_indncator.y < 0) {
                MoveDown();
            }
        }
        touch_position_indncator = Input.mousePosition;
        // Debug.Log();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        drag_start_position = Input.mousePosition;
        enddrag = false;
        //right = true;
       // left = true;
        //Debug.Log(drag_start_position);

        //Debug.Log();
       
    }

    public void OnEndDrag(PointerEventData eventData) {
        
        drag_end_position = Input.mousePosition;
        if (Orient == Orientation.vertical) {
            if (drag_start_position.y - drag_end_position.y > 0 && up == false) {
                down = true;
                enddrag = true;
            }
            if (drag_start_position.y - drag_end_position.y < 0 && down == false) {
                up = true;
                enddrag = true;
            }
        }

        if (Orient == Orientation.horizontal) {
            if (drag_start_position.x - drag_end_position.x > 0 && right == false) {
                left = true;
                enddrag = true;
            }
            if (drag_start_position.x - drag_end_position.x < 0 && left == false) {
                enddrag = true;
                right = true;
               
            }
        }

        // Debug.Log(drag_end_position);
    }

    void SelectedItem(int i) {
        ElementNumber = i;
        Selected_Item_Number.text = ElementNumber.ToString();
    }
}
