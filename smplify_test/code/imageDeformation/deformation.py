#! /usr/bin/env python
# -*- coding: utf-8 -*-

"""
Image deformation using moving least squares

@author: Jarvis ZHANG
@date: 2017/8/8
@editor: VS Code
"""

import os
import sys
import numpy as np
import matplotlib.pyplot as plt
from img_utils import (mls_affine_deformation, mls_affine_deformation_inv,
                       mls_similarity_deformation, mls_similarity_deformation_inv,
                       mls_rigid_deformation, mls_rigid_deformation_inv)


def show_example():
    img = plt.imread(os.path.join(sys.path[0], "double.jpg"))
    plt.imshow(img)
    plt.show()

def demo(fun, fun_inv, name):
    p = np.array([
        [30, 155], [125, 155], [225, 155],
        [100, 235], [160, 235], [85, 295], [180, 293]
    ])
    q = np.array([
        [42, 211], [125, 155], [235, 100],
        [80, 235], [140, 235], [85, 295], [180, 295]
    ])
    image = plt.imread(os.path.join(sys.path[0], "mr_big_ori.jpg"))

    plt.figure(figsize=(8, 6))
    plt.subplot(231)
    plt.axis('off')
    plt.imshow(image)
    plt.title("Original Image")
    if fun is not None:
        transformed_image = fun(image, p, q, alpha=1, density=1)
        plt.subplot(232)
        plt.axis('off')
        plt.imshow(transformed_image)
        plt.title("%s Deformation \n Sampling density 1"%name)
        transformed_image = fun(image, p, q, alpha=1, density=0.7)
        plt.subplot(235)
        plt.axis('off')
        plt.imshow(transformed_image)
        plt.title("%s Deformation \n Sampling density 0.7"%name)
    if fun_inv is not None:
        transformed_image = fun_inv(image, p, q, alpha=1, density=1)
        plt.subplot(233)
        plt.axis('off')
        plt.imshow(transformed_image)
        plt.title("Inverse %s Deformation \n Sampling density 1"%name)
        transformed_image = fun_inv(image, p, q, alpha=1, density=0.7)
        plt.subplot(236)
        plt.axis('off')
        plt.imshow(transformed_image)
        plt.title("Inverse %s Deformation \n Sampling density  0.7"%name)

    plt.tight_layout(w_pad=0.1)
    plt.show()

def demo2(fun):
    ''' 
        Smiled Monalisa  
    '''
    
    p = np.array([
        [184,240],[120,240]
    ])
    q = np.array([
        [187,240],[117,240]
    ])
    image = plt.imread(os.path.join(sys.path[0], "image4.png"))
    plt.subplot(121)
    plt.axis('off')
    plt.imshow(image)

    points=np.ones((20,10))
    transformed_image = fun(image, p, q, alpha=1,density=0.5)
    plt.subplot(122)
    plt.axis('off')
    plt.imshow(transformed_image)
    plt.tight_layout(w_pad=1.0, h_pad=1.0)
    #plt.show()
    plt.imsave("deformation_image4.png",transformed_image)


if __name__ == "__main__":
    #affine deformation
    #demo(mls_affine_deformation, mls_affine_deformation_inv, "Affine")
    #demo2(mls_affine_deformation_inv)

    #similarity deformation
    #demo(mls_similarity_deformation, mls_similarity_deformation_inv, "Similarity")
    #demo2(mls_similarity_deformation_inv)

    #rigid deformation
    #demo(mls_rigid_deformation, mls_rigid_deformation_inv, "Rigid")
    demo2(mls_rigid_deformation_inv)
