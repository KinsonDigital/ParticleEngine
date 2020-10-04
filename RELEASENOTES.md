# **Particle Engine Release Notes**

## **Particle Engine - Version 0.19.0**

### **New**

1. Added ability to dynamically update the **EasingRandomBehavior** **ChangeMin** and **ChangeMax** settings for updating the values dynamically during runtime.
   * These are done by setting the **UpdateChangeMin** and **UpdateChangeMax** properties of the **EasingRandomBehaviorSettings** class to an implementation of type **Func<float>**.  This will set the change min and max settings during runtime.

### **Other**

1. An additional **QA** environment has been added for QA testing.
2. Improved the build and release pipelines.
   * Every release archive is now retained on the **SoftwareReleases** share for the proper environment.

## **Particle Engine Tester - Version 1.1.0**

### **New**

1. Increased code coverage of code base to over **90%**.
2. Added logging of errors/exceptions during run time to an error log.
3. The **previous** and **next** scene buttons now are enabled and disabled appropriately when on the first or last scene.
4. The tester window is not able to be resized via the maximize and restore button and the mouse by grabbing the edge of the window.

---

## **Version 0.18.0**

### **New**

1. Added a new bursting effect to the particle system.
   * This effect can be used to increase the spawn rate for a set amount of time.  This make the particles "burst" onto the screen.
2. Created a new GUI application for testing the **Particle Engine**.

### **Developer Related Items**
1. Updated build and release pipelines to be setup for different environments.  A **DEV** and **QA** environment have been setup to improve the process of manual testing the particle engine.

---

## **Version 0.17.0**

### **New**

1. Added color transitioning to particles
   * This allows the user to set a start color and stop color and the particle will smoothly transition from that start color to the stop color based on an easing function.
2. Added ability for particles to fade over time using transparency
   * This allows the user to have the particles increase its transparency over time using easing functions.  This is very similar to the particle color transitioning feature except its for the particle color's **A (Alpha)** color component instead of the **RGB** color components.
3. Added ability to enable and disable the spawn rate of particles
   * This allows the user to have particles spawn at a particle rate or to turn it off entirely.  Disabled will spawn as many particles as possible

### **Developer Related Items**

1. Updated **MonoGame** nuget from **v3.7.0.7** to **v3.8.0.1641** in sand box project
   * This was to improve and keep the testing sand box developer project up to date.
2. Updated **Microsoft.NET.Test.Sdk** from **v16.7.0** to **v16.7.1**
3. Increased code coverage of code base
4. Updated unit testing **editorconfig** file to new version

---

## **Version 0.16.0**

### **Changes**

1. Updated PR templates
2. Fixed **stylecop.json** file setup in solution
3. Fixed the company name in all of the unit testing project file headers
4. Added warnings to **editorconfig** files to adjust coding standards

---

## **Version 0.15.0**

### **New**

1. Added code analyzers to the solution to enforce coding standards and keep code clean
   * This required adding nuget packages to allow the analyzers to run
		1. Microsoft.CodeAnalysis.FxCopAnalyzers - v3.3.0
		2. StyleCop.Analyzers - v1.1.118
   * Added/setup required **editorconfig** files with appropriate coding analyzer rules
   * Added **stylecop.json** files for the stylecop analyzer
2. Refactored code to meet code analyzer requirements
   * This was a very large code refactor

---

## **Version 0.14.0**

1. Refactored how a Particle object is updated to properly parse values in a way that improves debugging and to throw proper exceptions with parsing issues.

---

## **Version 0.13.0**

### **Developer Related Items**

1. Change build pipelines by changing **YAML** files to use stages with jobs and tasks.

---

## **Version 0.12.0**

### **New**

1. Added new particle behavior that allows the ability to choose from a list of random colors to apply to a particle for the lifetime of a particle.
   * Use the **RandomColorBehavior** class to give a particle this behavior.

### **Misc**

1. Updated copyright license to **Kinson Digital**
2. Changed param name of the **TextureLoader.LoadTexture()** method from **textureName** to **imageFilePath**


## **Version 0.11.0 (HotFix)**

1. Added ability to use an **IParticleTexture** to help introduce the ability to be able to implement proper disposal of managed and unmanaged resources
2. Added proper disposal pattern across code base


## **Version 0.10.2**

1. Add a parameter-less constructor to the **ParticleEffect** class.
   * This was required to allow JSON serialization and deserialization for further development of the **Particle Maker** application


## **Version 0.10.1**

### **Developer Related Items**

1. Added a **CONTRIBUTING.md** file to the project.
   * This will enable developers to be able to know how to contribute to the project.
2. Improved **develop** and **master** build pipelines
   * This involved changing the **YAML** files to split various parts of the build process into stages and jobs.


## **Version 0.10.0**

### **Developer Related Items**

1. Set all solution projects to use **C# v8.0**
2. Simple updates to build pipelines via YAML files
3. Made layout improvements to **RELEASE NOTES** document


## **Version 0.9.3**

### **Developer Related Items**

1. Update nuget package information


## **Version 0.9.2**

### **What's New**
1. N/A


### **Developer Related Items**

1. Simple cleanup


## **Version 0.9.0**

### **What's New**

1. Everything is new.  This is the first initial release notes


### **Developer Related Items**

1. Added an assembly version tag to the project file.
   * This will help with versioning of the nuget package
2. Added a YAML file for the development build pipeline.
   * This gives the ability to have **Continuous Integration(CI)** in **Azure Pipelines**
3. Added a YAML file for the production build and release pipelines.
   * This gives the ability to have **Continuous Integration(CI/CD)** for **Azure Pipelines**
4. Added an MIT license to the project.
   * Refer to the **LICENSE.md** file for info
5. Added release notes file to the solution/project.
   * Refer to this very file that you are currently reading!!
6. Added a readme file to the solution/project.
   * Refer to the **README.md** file for info
