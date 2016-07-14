using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class SliderScript : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerUpHandler,IPointerClickHandler{
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
    List<MoveSlide> Slides;
    Transform[] imageTransforms;

    Vector3 firstSlidePosition;
    Vector3 LastSlidePosition;
 
    Vector3 imagepos;
    float[] aimagepositions;
    int step = 1;
    bool left,right,up,down;
    bool enddrag;
    bool slideAllMove=true;
    bool slideAllDrag = true;
    bool dragPos;
    bool drag=true;
    int move_right_step=0;
    int move_left_step=0;
    int move_down_step = 0;
    int move_up_step = 0;

    MoveSlide _slideForScale;
    MoveSlide _slideForUnscale;
    // Use this for initialization
    int[] testarray;
    public enum Orientation {
        vertical,
        horizontal
    }

    float[] ReversArray( float [] a) {
        float[] b = new float[a.Length];
        for (int i=0;i<a.Length;++i) {
            if (i == a.Length - 1) {
                b[0] = a[i];
            }
            else {
            //    Debug.Log(b[i]);
                b[i+1] = a[i];           
            }
           
        }
        return b;

    }
    void Start () {
        //testarray = new int[8];
        //for (int i=0;i<testarray.Length;++i) {
        //    testarray[i] = i;
        //}
      
      
        //for (byte i = 0; i <4; ++i) {

        // // testarray = ReversArray(testarray);
        //}

        //for (int i = 0; i < testarray.Length; ++i) {
        
        //    Debug.Log(testarray[i]);
        //}
        //  Debug.Log(Mathf.RoundToInt(9f/2));
        Slides = new List<MoveSlide>();
        foreach (GameObject image in images) {
            image.AddComponent<MoveSlide>();
           
            Slides.Add(image.GetComponent<MoveSlide>());
        }
        imageTransforms = new Transform[images.Length];
        for (int i = 0;i<images.Length;++i)  {
            imageTransforms[i] = images[i].GetComponent<Transform>();
        }
        
        if (Drag_Only==true && UpButton != null && DownButton != null && LeftButton != null && RightButton != null) {
            UpButton.gameObject.SetActive(false);
            DownButton.gameObject.SetActive(false);
            LeftButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(false);
        }
        #region  Horizontal Orientation
        if (Orient == Orientation.horizontal) {
            imagestep =Mathf.Round( imagestep * (Screen.width / 1280f));
            if (UpButton != null && DownButton != null) {
                RightButton.onClick.AddListener(MoveRightButton);
                LeftButton.onClick.AddListener(MoveLeftButton);
                UpButton.gameObject.SetActive(false);
                DownButton.gameObject.SetActive(false);
            }
            aimagepositions = new float[images.Length];
            if (images.Length % 2 == 0) {

                firstSlidePosition = new Vector3(middle_slide_position.localPosition.x - imagestep * images.Length / 2,
                                                     middle_slide_position.localPosition.y,
                                                     middle_slide_position.localPosition.z);
                LastSlidePosition = new Vector3(middle_slide_position.localPosition.x + imagestep * (images.Length / 2 - 1),
                                                     middle_slide_position.localPosition.y,
                                                     middle_slide_position.localPosition.z);
            }
            else {

                firstSlidePosition = new Vector3(middle_slide_position.localPosition.x - imagestep * Mathf.RoundToInt(images.Length / 2),
                                                    middle_slide_position.localPosition.y,
                                                    middle_slide_position.localPosition.z);
                LastSlidePosition = new Vector3(middle_slide_position.localPosition.x + imagestep * Mathf.RoundToInt(images.Length / 2),
                                                    middle_slide_position.localPosition.y,
                                                    middle_slide_position.localPosition.z);
            }
          //  Debug.Log(middle_slide_position.localPosition);
            imageTransforms[0].localPosition = firstSlidePosition;
           
            
          
            aimagepositions[0] = firstSlidePosition.x;
            imagepos = firstSlidePosition;
            minScale = images[0].transform.localScale.x;
           
            for (byte i = 1; i < images.Length; ++i) {
               // Debug.Log(imagepos);
                imagepos += Vector3.right * imagestep;
                
                imageTransforms[i].localPosition = imagepos;
                aimagepositions[i] = imageTransforms[i].localPosition.x;
              
            }

            if (Managers._gameManager.sceneNumber> Mathf.RoundToInt(images.Length / 2)) {
                for (byte i = 0; i < Mathf.Abs(Mathf.RoundToInt(images.Length / 2) - Managers._gameManager.sceneNumber); i++) {

                    aimagepositions = ReversArray(aimagepositions);
                }
                move_left_step = Mathf.Abs(Mathf.RoundToInt(images.Length / 2) - Managers._gameManager.sceneNumber);
            }
            if (Managers._gameManager.sceneNumber < Mathf.RoundToInt(images.Length / 2)) {
                for (byte i = 0; i < images.Length - Mathf.Abs(Mathf.RoundToInt(images.Length / 2) - Managers._gameManager.sceneNumber); i++) {

                    aimagepositions = ReversArray(aimagepositions);
                }
                move_left_step = images.Length - Mathf.Abs(Mathf.RoundToInt(images.Length / 2) - Managers._gameManager.sceneNumber);
            }
          
            for (byte i = 0; i < images.Length; ++i) {

                imageTransforms[i].localPosition = new Vector3(aimagepositions[i], imageTransforms[i].localPosition.y, imageTransforms[i].localPosition.z);

            }
           
           //if (move_right_step > images.Length) {
           //     move_right_step =move_right_step-images.Length+1;
           //     //move_right_step = 0;
           // }

            imageTransforms[Managers._gameManager.sceneNumber].localScale = Vector3.one * 1.2f;
            Debug.Log(Managers._gameManager.sceneNumber);

            Debug.Log(move_left_step);
            SelectedItem(Managers._gameManager.sceneNumber);
            //if (images.Length % 2 == 0) {

            //    for (byte i = 0; i < images.Length - Managers._gameManager.sceneNumber - Mathf.RoundToInt(images.Length / 2); ++i) {

            //    }
            //    move_left_step = images.Length / 2;

            //}
            //else {
            //    for (byte i = 0; i < images.Length; ++i) {


            //    }
            //    move_left_step = Mathf.RoundToInt(images.Length / 2);


            //}

            for (byte i = 0; i < images.Length; ++i) {
                aimagepositions[i] = imageTransforms[i].localPosition.x;

            }
           // Array.Sort(,);

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
   // if (Drag_Only == false||enddrag==true) {
            if (right == true) {
                MoveRight();
            }
            if (left == true) {

             Debug.Log("Moveleft");
                MoveLeft();
            }

            if (down == true) {

                MoveDown();
            }

            if (up == true) {

                MoveUp();
            }
    // }
        


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


        if (slideAllMove == true) {
            int ii = 0;
            foreach (MoveSlide slide in Slides) {
               

                Debug.Log(middle_slide_position.localPosition.x - imageTransforms[ii].localPosition.x);
                if (middle_slide_position.localPosition.x - imageTransforms[ii].localPosition.x <= imagestep && middle_slide_position.localPosition.x - imageTransforms[ii].localPosition.x > 0) {
                    imageTransforms[ii].SetAsLastSibling();
                    slide.Scale(Vector3.one * 1.4f);
                    SelectedItem(ii);
                 //   Debug.Log(ii);
                }
                else {
                    slide.UnScale();
                }
                slide.MoveRight(aimagepositions[ii] + imagestep);

                ii++;
            }
            slideAllMove = false;
        }

       

        Debug.Log("R");
        if (imageTransforms[0].localPosition.x == aimagepositions[0] + imagestep) {//chek position if position more than this pos + step

            Debug.Log("Moveright");
            foreach (MoveSlide slide in Slides) {

                slide.StopAllTweens();
            }
            //  DOTween.Clear();
            if (move_right_step >= images.Length) {
                move_right_step = 0;//image array step element go to zero
            }
            if (move_left_step > 0) {
                // Slides[move_left_step - 1].enabled = false;
                Debug.Log("GoToFirstPos");
                images[move_left_step - 1].transform.localPosition = firstSlidePosition;
                move_left_step--;

               
            }
            else {
                // Slides[images.Length - 1 - move_right_step].enabled = false;
             //   Debug.Log("GotofirstPos");
                images[images.Length - 1 - move_right_step].transform.localPosition = firstSlidePosition;// go to firstslideposition
                move_right_step++;

               
            }

            for (byte i = 0; i < images.Length; ++i) {
             //   Debug.Log(imageTransforms[i].localPosition.x);
                aimagepositions[i] = imageTransforms[i].localPosition.x;//save pos of all image  array elements 
                
            }

            right = false;
            slideAllMove = true;
            drag = true;
           // Debug.Log("Right");
        }

       // drag = false;
    }

    void MoveLeft() {//Left Movement Method

        if (slideAllMove==true) {
            int ii = 0;
            foreach (MoveSlide slide in Slides) {

                slide.MoveLeft(aimagepositions[ii] - imagestep);
                if (imageTransforms[ii].localPosition.x - middle_slide_position.localPosition.x <= imagestep && imageTransforms[ii].localPosition.x - middle_slide_position.localPosition.x > 0) {
                    imageTransforms[ii].SetAsLastSibling();
                    slide.Scale(Vector3.one * 1.4f);
                    SelectedItem(ii);
                }
                else {
                    slide.UnScale();
                }

                ii++;
            }
            slideAllMove = false;
        }

        Debug.Log("L");


        if (imageTransforms[0].localPosition.x == aimagepositions[0] - imagestep) {

            foreach (MoveSlide slide in Slides) {//disable all tweens on slides.If tween will be enable coords of slide will be not correct

                slide.StopAllTweens();
            }
            // Debug.Log(Mathf.Round(imageTransforms[0].localPosition.x));
            if (move_left_step >= images.Length) {
                move_left_step = 0;
            }
            if (move_right_step > 0) {
              //  imageTransforms[images.Length - move_right_step].SetAsFirstSibling();
                imageTransforms[images.Length - move_right_step].localPosition = LastSlidePosition;
                
                move_right_step--;
            }
            else {
               // imageTransforms[move_left_step].SetAsFirstSibling();
                imageTransforms[move_left_step].localPosition = LastSlidePosition;// new Vector3(aimagepositions[0], 
                move_left_step++;
            }

            for (byte i = 0; i < images.Length; ++i) {
                aimagepositions[i] = imageTransforms[i].localPosition.x;

               // Debug.Log(imageTransforms[i].localPosition.x);
            }
            drag = true;
            left = false;
            slideAllMove = true;

            Debug.Log("LeftFinal");
        }
      //  drag = false;


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


    void RightDrag() {
      //  if (drag==false) {
      //      return;
      //  }
      ////  drag = false;
      ////  if (slideAllMove== true) {
      //      int ii = 0;
      //      foreach (MoveSlide slide in Slides) {
      //          // slide.MoveRight(aimagepositions[ii] + imagestep);
      //          slide.RightDrag(1);
      //          if (middle_slide_position.position.x - imageTransforms[ii].position.x < imagestep && middle_slide_position.position.x - imageTransforms[ii].position.x > 0) {
      //              slide.Scale(Vector3.one * 1.2f);
      //             // _slideForScale = slide;
      //         // slide.ScaleDrag(0.005f);
      //          }
      //          else {
      //              slide.UnScale();
      //          }


      //          ii++;
      //      }
      //     // slideAllMove = false;
      // // }


      //  // Debug.Log(imageTransforms[0].localPosition.x);
      //  if (imageTransforms[0].localPosition.x == aimagepositions[0] + imagestep) {//chek position if position more than this pos + step

      //      Debug.Log("RightDrag");
      //      foreach (MoveSlide slide in Slides) {

      //          slide.StopAllTweens();
      //      }
      //      //  DOTween.Clear();
      //      if (move_right_step >= images.Length) {
      //          move_right_step = 0;//image array step element go to zero
      //      }
      //      if (move_left_step > 0) {
      //          // Slides[move_left_step - 1].enabled = false;
      //          Debug.Log("GoToFirstPos");
      //          images[move_left_step - 1].transform.localPosition = firstSlidePosition;
      //          move_left_step--;


      //      }
      //      else {
      //          // Slides[images.Length - 1 - move_right_step].enabled = false;
      //         // Debug.Log("GotofirstPos");
      //          images[images.Length - 1 - move_right_step].transform.localPosition = firstSlidePosition;// go to firstslideposition
      //          move_right_step++;


      //      }

      //      for (byte i = 0; i < images.Length; ++i) {
      //        //  Debug.Log(imageTransforms[i].localPosition.x);
      //          aimagepositions[i] = imageTransforms[i].localPosition.x;//save pos of all image  array elements 

      //      }
      //     // dragPos = false;
      //     // right = false;
      //    //  slideAllMove = true;
      //  //    Debug.Log("Right");
      //  }


    }

    void LeftDrag() {//Left Movement Method
                     //    if (drag == false) {
                     //        return;
                     //    }
                     //   // drag = false;
                     //    int ii = 0;
                     //    foreach (MoveSlide slide in Slides) {
                     //        slide.LeftDrag(1);
                     //     if (imageTransforms[ii].position.x - middle_slide_position.position.x < imagestep && imageTransforms[ii].position.x - middle_slide_position.position.x > 0) {
                     //            slide.Scale(Vector3.one * 1.2f);
                     //        }
                     //        else {
                     //            slide.UnScale();
                     //        }

        //        ii++;
        //    }




        //  //  Debug.Log(Mathf.Round(imageTransforms[0].localPosition.x) == Mathf.Round(aimagepositions[0] - imagestep));

        //    if (imageTransforms[0].localPosition.x == aimagepositions[0] - imagestep) {

        //        Debug.Log("LeftDrag");
        //        foreach (MoveSlide slide in Slides) {//disable all tweens on slides.If tween will be enable coords of slide will be not correct

        //            slide.StopAllTweens();
        //        }
        //        // Debug.Log(Mathf.Round(imageTransforms[0].localPosition.x));
        //        if (move_left_step >= images.Length) {
        //            move_left_step = 0;
        //        }
        //        if (move_right_step > 0) {
        //            //  imageTransforms[images.Length - move_right_step].SetAsFirstSibling();
        //            imageTransforms[images.Length - move_right_step].localPosition = LastSlidePosition;

        //            move_right_step--;
        //        }
        //        else {
        //            // imageTransforms[move_left_step].SetAsFirstSibling();
        //            imageTransforms[move_left_step].localPosition = LastSlidePosition;// new Vector3(aimagepositions[0], 
        //            move_left_step++;
        //        }

        //        for (byte i = 0; i < images.Length; ++i) {
        //            aimagepositions[i] = imageTransforms[i].localPosition.x;

        //            // Debug.Log(imageTransforms[i].localPosition.x);
        //        }

        //       // left = false;
        //     //   slideAllMove = true;

        //    }


    }
    public float SmoothScale(float min, float max, float step) {
        float value = 0;
        min += step;
        value = min;
        if (min >= max) {
            value = max;
        }

        // Debug.Log(value);
        return value;
    }

    public void OnDrag(PointerEventData eventData) {
        if (Orient == Orientation.horizontal) {
            if (Input.mousePosition.x - touch_position_indncator.x > 0) {

                // MoveRight();
                //for (int i = 0; i <= 10; ++i) {
                  
                //   RightDrag();
                //   //Scale();
                //   //Unscale();
                //}
                
             
            }
            if (Input.mousePosition.x - touch_position_indncator.x < 0) {
                // MoveLeft();
                //   LeftDrag();
               //for (int i = 0; i <=10; ++i) {

               //    LeftDrag();
               //}
            }
        }

        if (Orient == Orientation.vertical) {
            if (Input.mousePosition.y - touch_position_indncator.y > 0) {
             //   MoveUp();
            }
            if (Input.mousePosition.y - touch_position_indncator.y < 0) {
               // MoveDown();
            }
        }
        touch_position_indncator = Input.mousePosition;
        // Debug.Log();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (imageTransforms[ElementNumber].position.x==middle_slide_position.position.x) {
            drag = true;
        }
        drag_start_position = Input.mousePosition;
        enddrag = false;
        //right = true;
       // left = true;
        //Debug.Log(drag_start_position);

        //Debug.Log();
       
    }

    public void OnEndDrag(PointerEventData eventData) {
       drag = false;
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

        //if (Orient == Orientation.horizontal) {
        //    if (imageTransforms[ElementNumber].position.x< middle_slide_position.position.x && right == false) {
        //        left = true;
        //        enddrag = true;
        //    }
        //    if (imageTransforms[ElementNumber].position.x > middle_slide_position.position.x && left == false) {
        //        enddrag = true;
        //        right = true;

        //    }

        //}

        // Debug.Log(drag_end_position);
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (Input.mousePosition.x<middle_slide_position.position.x) {
       //     left = true;
        }

        Debug.Log("UpLeft");
    }

    void Scale() {
     //   _slideForScale.ScaleDrag(0.005f);
    }
    void Unscale() {

   }

    void SelectedItem(int i) {
        ElementNumber = i;
        Selected_Item_Number.text = ElementNumber.ToString();
    }

    public void OnPointerClick(PointerEventData eventData) {
       
    }
}
