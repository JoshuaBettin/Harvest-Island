using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDo
    {
    /* 

    - AgentWeapon field to see which weapon is equipped                                 done  
    - animating player movement                                                         done
    - animating weapon usage                                                            done
    - separating equipping weapon from using weapon                                     done

    - separating player inventories                                                     done     (inventories alreay separated, only pick up system had to be adjusted)                               
    - separating player healthbars (i guess)                                            done        "

    - player pickup sounds                                                              done    but not separated in order for others to get feedback if someone picked something up
                                                                                                

    think about next steps:

    - attacking damages players                                                         done
    - revive players after killing                                                      done
    - sound when player gets damaged                                                    done                 
    - animation when player gets damaged                                                done    changed spriterenderer.color via RPC instead of animation
    - death panel countdown until revive                                                done
    - change player position before revive                                              done

    - login name for each player                                                        done                                             
    - kill count for each player                                                        done
    - drop items on player death                                                        done
    - disable player names when dead                                                    done
    - attack speed limit                                                                done
    - fix bug where health is falsly displayed after eating apple                       done    i think
    - fix bug where items are only collected by one player                              done    -> problem was created when making network item out of normal item

    - join older rooms in lobby                                                         done

    - background music                                                                  done    (temporarily)

    - change lobby ui                                                                   done    (temporarily)

    - ingame menu
    - leave game per escape button

    - different hit sound for other players

    // bugs:

    - ui mousefollower position fixen
    - camera moving on death

    // better code:

    - change animation triggers to photon RPCs
    - change time between attacks to attackspeed
     
    // others: 

    - remove input action from assets because not used

     */
}
