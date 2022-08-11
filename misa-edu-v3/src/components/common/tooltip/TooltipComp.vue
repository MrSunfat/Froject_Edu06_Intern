<template>
  <div class="tooltip">
    <p class="subtitle-two content">{{ contentTooltip }}</p>
  </div>
</template>

<script>
export default {
  name: "TooltipComp",
  props: {
    contentTooltip: {
      type: String,
      default: "Test Tooltip",
    },
  },
};
</script>

<style>
.show-tooltip {
  position: relative;
}

.tooltip {
  position: absolute;
  top: calc(100% + 8px);
  left: 50%;
  transform: translateX(-50%);
  width: 100%;
  min-width: 90px;
  padding: 12px;
  border-radius: 4px;
  background: #fff;
  box-shadow: 0 0 10px #d5d5d5;
  z-index: 3;
  display: none;
}

.tooltip::after {
  position: absolute;
  content: "";
  top: -4px;
  left: 50%;
  transform: translateX(-50%);
  width: 0;
  height: 0;
  border-left: 5px solid transparent;
  border-right: 5px solid transparent;
  border-bottom: 5px solid #fff;
}
/* 
.tooltip::before {
  position: absolute;
  content: "";
  top: 0;
  height: 50px;
  left: 0;
  right: 0;
  background: #000;
} */

.tooltip > .content {
  color: #000;
  text-align: center;
  margin: 0;
}

.show-tooltip:hover .tooltip {
  display: block;
}
</style>